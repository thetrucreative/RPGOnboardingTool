document.addEventListener('DOMContentLoaded', function () {
    const characterForm = document.getElementById('characterForm');
    if (!characterForm) return;

    const characterId = document.getElementById('characterId').value;
    const characterResult = document.getElementById('characterResult');
    const raceSelect = document.getElementById('race');
    const trainingPackageSelect = document.getElementById('trainingPackage');
    const statsContainer = document.getElementById('stats-container');
    const skillsContainer = document.getElementById('skills-container');
    const traitsContainer = document.getElementById('traits-container');
    const equipmentContainer = document.getElementById('equipment-container');
    const generalItemsContainer = document.getElementById('general-items-container');
    const statPointsSpan = document.getElementById('statPoints');
    const skillPointsSpan = document.getElementById('skillPoints');
    const creditsSpan = document.getElementById('credits');
    const financeChipCheckbox = document.getElementById('finance-chip');

    let racesData = [];
    let packagesData = [];
    let traitsData = [];
    let equipmentData = [];
    let skillsData = [];
    let generalItemsData = [];
    let characterData = {};

    let statPoints = 0;
    let skillPoints = 0;
    let initialCredits = 0;
    let encumbrance = 0;

    // Track items in cart separately from display quantities  
    let cartItems = new Map(); // equipmentId -> quantity

    // Fetch all necessary data
    Promise.all([
        fetch('/api/CharacterApi/races').then(res => res.json()),
        fetch('/api/CharacterApi/training-packages').then(res => res.json()),
        fetch('/api/CharacterApi/traits').then(res => res.json()),
        fetch('/api/CharacterApi/equipment').then(res => res.json()),
        fetch('/api/CharacterApi/skills').then(res => res.json()),
        fetch('/api/CharacterApi/general-items').then(res => res.json()),
        fetch(`/api/CharacterApi/${characterId}`)
    ]).then(async ([races, packages, traits, equipment, skills, generalItems, characterResponse]) => {
        racesData = races;
        packagesData = packages;
        traitsData = traits;
        equipmentData = equipment;
        skillsData = skills;
        generalItemsData = generalItems;
        characterData = await characterResponse.json();

        characterData.rowVersion = characterData.rowVersion || null;

        statPoints = characterData.statPointsRemaining;
        skillPoints = characterData.skillPointsRemaining;
        initialCredits = characterData.credits + calculateInitialCost(characterData);

        populateForm();
        updateResourceDisplay();
        validateResources();
    }).catch(error => {
        console.error('Error fetching initial data:', error);
        characterResult.textContent = 'Error loading character data. Please try again later.';
        characterResult.className = 'error';
    });

    function calculateInitialCost(character) {
        let totalCost = 0;
        if (character.characterEquipment) {
            character.characterEquipment.forEach(item => {
                if (item.equipmentItem) {
                    totalCost += item.equipmentItem.cost * item.quantity;
                }
            });
        }
        if (character.characterGeneralItems) {
            character.characterGeneralItems.forEach(item => {
                if (item.generalItem) {
                    totalCost += item.generalItem.cost * item.quantity;
                }
            });
        }
        if (character.hasFinanceChip) {
            totalCost += 100;
        }
        return totalCost;
    }

    function populateForm() {
        document.getElementById('name').value = characterData.name;
        financeChipCheckbox.checked = characterData.hasFinanceChip;

        // Populate dropdowns
        racesData.forEach(race => {
            const option = document.createElement('option');
            option.value = race.id;
            option.textContent = race.name;
            if (race.id === characterData.raceId) {
                option.selected = true;
            }
            raceSelect.appendChild(option);
        });

        packagesData.forEach(pkg => {
            const option = document.createElement('option');
            option.value = pkg.id;
            option.textContent = pkg.name;
            if (pkg.id === characterData.trainingPackageId) {
                option.selected = true;
            }
            trainingPackageSelect.appendChild(option);
        });

        if (characterData.characterRace && characterData.characterRace.statLimits) {
            populateStats(characterData.characterRace.statLimits, characterData.stats);
        }
        populateSkills(characterData.skills);
        populateTraits(characterData.characterTraits);
        if (characterData.characterEquipment) {
            populateEquipment(characterData.characterEquipment);
        }
        populateGeneralItems(characterData.characterGeneralItems);

        // Trigger change to ensure everything is correctly displayed
        raceSelect.dispatchEvent(new Event('change'));
    }

    raceSelect.addEventListener('change', function() {
        const selectedRace = racesData.find(r => r.id === this.value);
        if (selectedRace) {
            populateStats(selectedRace.statLimits);
            financeChipCheckbox.disabled = !selectedRace.canHaveFinanceChip;
            if (!selectedRace.canHaveFinanceChip) {
                financeChipCheckbox.checked = false;
            }
            resetAndApplyRacialSkills(selectedRace.speciesSkills);
        }
    });

    trainingPackageSelect.addEventListener('change', function() {
        const selectedPackage = packagesData.find(p => p.id === this.value);
        if (selectedPackage) {
            applyPackageSkills(selectedPackage.packageSkills);
        } else {
            raceSelect.dispatchEvent(new Event('change'));
        }
    });

    function populateStats(statLimits, existingStats = []) {
        statsContainer.innerHTML = '';
        statLimits.forEach(limit => {
            const existingStat = existingStats.find(s => s.type.toLowerCase() === limit.statType.toLowerCase());
            const value = existingStat ? existingStat.value : limit.minValue;
            const statDiv = document.createElement('div');
            statDiv.innerHTML = `
                <label>${limit.statType}</label>
                <button type="button" class="stat-decrease" data-stat-type="${limit.statType}">-</button>
                <input type="number" id="stat-${limit.statType}" value="${value}" min="${limit.minValue}" max="${limit.maxValue}" data-stat-type="${limit.statType}" readonly>
                <button type="button" class="stat-increase" data-stat-type="${limit.statType}">+</button>
            `;
            statsContainer.appendChild(statDiv);
        });
    }

    function populateSkills(existingSkills = []) {
        const tabsContainer = document.getElementById('skills-tabs');
        tabsContainer.innerHTML = '';
        skillsContainer.innerHTML = '';

        const skillGroups = {
            STR: [], DEX: [], KNOW: [], CONC: [], CHA: [], COOL: []
        };

        // Group skills by their related stat
        skillsData.forEach(skill => {
            const statName = getStatName(skill.relatedStat);
            if (skillGroups[statName]) {
                skillGroups[statName].push(skill);
            }
        });

        // Create tabs for each stat group that has skills
        Object.keys(skillGroups).forEach((group, index) => {
            if (skillGroups[group].length > 0) { // Only create tabs for groups with skills
                const tab = document.createElement('button');
                tab.type = 'button'; // Prevent form submission
                tab.className = 'tab-link';
                tab.textContent = group;
                tab.dataset.group = group;
                if (index === 0) {
                    tab.classList.add('active');
                }
                tabsContainer.appendChild(tab);
            }
        });

        // Add event listener for tab clicks
        tabsContainer.addEventListener('click', (e) => {
            if (e.target.classList.contains('tab-link')) {
                tabsContainer.querySelectorAll('.tab-link').forEach(tab => tab.classList.remove('active'));
                e.target.classList.add('active');
                displaySkillsForGroup(e.target.dataset.group, skillGroups, existingSkills);
            }
        });

        // Display the first group's skills initially
        const firstGroupWithSkills = Object.keys(skillGroups).find(group => skillGroups[group].length > 0);
        if (firstGroupWithSkills) {
            displaySkillsForGroup(firstGroupWithSkills, skillGroups, existingSkills);
        }
    }

    // Helper function to convert stat enum number to name (copied from site.js)
    function getStatName(statValue) {
        const statMap = {
            0: 'STR',    // Strength
            1: 'DEX',    // Dexterity  
            2: 'KNOW',   // Knowledge
            3: 'CONC',   // Concentration
            4: 'CHA',    // Charisma
            5: 'COOL'    // Coolness
        };
        return statMap[statValue] || 'UNKNOWN';
    }

    function displaySkillsForGroup(group, skillGroups, existingSkills = []) {
        skillsContainer.innerHTML = '';
        skillGroups[group].forEach(skill => {
            const existingSkill = existingSkills.find(s => s.name === skill.name);
            const rank = existingSkill ? existingSkill.rank : 0;
            const skillDiv = document.createElement('div');
            skillDiv.innerHTML = `
                <label for="skill-${skill.id}">${skill.name}</label>
                <div class="point-controls">
                    <button type="button" class="skill-decrease" data-skill-id="${skill.id}">-</button>
                    <input type="number" id="skill-${skill.id}" value="${rank}" min="0" max="3" data-skill-id="${skill.id}" readonly>
                    <button type="button" class="skill-increase" data-skill-id="${skill.id}">+</button>
                </div>
            `;
            skillsContainer.appendChild(skillDiv);
        });
    }

    function populateTraits(existingTraits = []) {
        traitsContainer.innerHTML = '';
        const filter = document.getElementById('trait-filter').value;

        traitsData
            .filter(trait => filter === 'all' || trait.type.toLowerCase() === filter)
            .forEach(trait => {
            const isChecked = existingTraits.some(t => t.traitId === trait.id);
            const traitDiv = document.createElement('div');
            traitDiv.className = 'trait';
            traitDiv.innerHTML = `
                <input type="checkbox" id="trait-${trait.id}" data-trait-id="${trait.id}" data-point-cost="${trait.basePointCost}" ${isChecked ? 'checked' : ''}>
                <label for="trait-${trait.id}">${trait.name} (${trait.basePointCost} points)</label>
                <p>${trait.description}</p>
            `;
            traitsContainer.appendChild(traitDiv);
        });
    }

    document.getElementById('trait-filter').addEventListener('change', () => populateTraits(characterData.characterTraits));

    function populateGeneralItems(existingItems = []) {
        generalItemsContainer.innerHTML = '';
        generalItemsData.forEach(item => {
            const existingItem = existingItems.find(e => e.generalItemId === item.id);
            const quantity = existingItem ? existingItem.quantity : 0;
            const itemDiv = document.createElement('div');
            itemDiv.className = 'form-group';
            itemDiv.innerHTML = `
                <label for="general-item-${item.id}">${item.name} (${item.cost} credits)</label>
                <input type="number" id="general-item-${item.id}" value="${quantity}" min="0" data-item-id="${item.id}" data-cost="${item.cost}" data-weight="${item.weight}">
            `;
            generalItemsContainer.appendChild(itemDiv);
        });
    }

    function populateEquipment(existingEquipment = []) {
        equipmentContainer.innerHTML = '';
        
        // Initialize cart with existing equipment
        cartItems.clear();
        existingEquipment.forEach(item => {
            if (item.equipmentItemId && item.quantity > 0) {
                cartItems.set(item.equipmentItemId, item.quantity);
            }
        });
        
        equipmentData.forEach(item => {
            const cartQuantity = cartItems.get(item.id) || 0;
            const itemDiv = document.createElement('div');
            itemDiv.className = 'equipment-card';
            const imageUrl = item.imageUrl ? item.imageUrl : 'https://upload.wikimedia.org/wikipedia/commons/thumb/1/14/No_Image_Available.svg/2048px-No_Image_Available.svg.png';
            itemDiv.innerHTML = `
                <img src="${imageUrl}" alt="${item.name}" class="equipment-image">
                <h4>${item.name}</h4>
                <p>${item.description}</p>
                <div class="equipment-details">
                    <span>Cost: ${item.cost}c</span>
                    <span>Weight: ${item.weight}</span>
                </div>
                <div class="equipment-quantity">
                    <label for="equip-${item.id}">Quantity:</label>
                    <input type="number" id="equip-${item.id}" value="0" min="0" data-equip-id="${item.id}" data-cost="${item.cost}" data-weight="${item.weight}">
                </div>
                <div class="cart-info" style="margin: 5px 0; font-size: 0.9em; color: #666;">
                    <span>In Cart: ${cartQuantity}</span>
                </div>
                <button type="button" class="btn add-to-cart-btn" data-equip-id="${item.id}">Add to Cart</button>
            `;
            equipmentContainer.appendChild(itemDiv);
        });
    }

    function calculateTotalCost() {
        let totalCost = 0;
        encumbrance = 0; // Reset encumbrance before recalculating
        
        // Calculate from cart items instead of input values
        cartItems.forEach((quantity, equipId) => {
            const equipment = equipmentData.find(item => item.id === equipId);
            if (equipment) {
                totalCost += equipment.cost * quantity;
                encumbrance += equipment.weight * quantity;
            }
        });

        generalItemsContainer.querySelectorAll('input[type="number"]').forEach(input => {
            const quantity = parseInt(input.value) || 0;
            const cost = parseInt(input.dataset.cost) || 0;
            const weight = parseFloat(input.dataset.weight) || 0;
            totalCost += quantity * cost;
            encumbrance += quantity * weight;
        });
        
        return totalCost;
    }

    //adapted from site.js
    function updateResourceDisplay() {
        statPointsSpan.textContent = statPoints;
        skillPointsSpan.textContent = skillPoints;
        
        // Calculate base credits after equipment costs
        let displayCredits = initialCredits - calculateTotalCost();
        
        // Finance chip ADDS 100 credits to the display
        if (financeChipCheckbox.checked) {
            displayCredits += 100;
        }
        
        creditsSpan.textContent = displayCredits;
        document.getElementById('encumbrance').textContent = encumbrance;
    }

    function validateResources() {
        const remainingCredits = initialCredits - calculateTotalCost();
        const submitButton = characterForm.querySelector('button[type="submit"]');

        if (remainingCredits < 0) {
            creditsSpan.parentElement.style.color = 'red';
            submitButton.disabled = true;
        } else {
            creditsSpan.parentElement.style.color = '';
            submitButton.disabled = false;
        }
    }

    function resetAndApplyRacialSkills(racialSkills, existingSkills = []) {
        skillsContainer.querySelectorAll('input[type="number"]').forEach(input => {
            const skillId = input.dataset.skillId;
            const racialSkill = racialSkills.find(rs => {
                const skillDef = skillsData.find(s => s.id === skillId);
                return skillDef && skillDef.name === rs.skillName;
            });
            const existingSkill = existingSkills.find(es => {
                const skillDef = skillsData.find(s => s.id === skillId);
                return skillDef && skillDef.name === es.name;
            });

            if (existingSkill) {
                input.value = existingSkill.rank;
            } else if (racialSkill) {
                input.value = racialSkill.rank;
            } else {
                input.value = "0";
            }
        });
        recalculateSkillPoints();
    }

    function applyPackageSkills(packageSkills, existingSkills = []) {
        const selectedRace = racesData.find(r => r.id === raceSelect.value);
        if (selectedRace) {
            resetAndApplyRacialSkills(selectedRace.speciesSkills, existingSkills);
        }

        packageSkills.forEach(pkgSkill => {
            const skillDef = skillsData.find(s => s.name === pkgSkill.skillName);
            if (skillDef) {
                const input = document.getElementById(`skill-${skillDef.id}`);
                if (input) {
                    let currentRank = parseInt(input.value) || 0;
                    input.value = currentRank + pkgSkill.rank;
                }
            }
        });
        recalculateSkillPoints();
    }

    function recalculateSkillPoints() {
        let spentPoints = 0;
        skillsContainer.querySelectorAll('input[type="number"]').forEach(input => {
            const rank = parseInt(input.value) || 0;
            for (let i = 1; i <= rank; i++) {
                spentPoints += i;
            }
        });
        
        let traitPoints = 0;
        traitsContainer.querySelectorAll('input[type="checkbox"]:checked').forEach(checkbox => {
            traitPoints += parseInt(checkbox.dataset.pointCost) || 0;
        });

        const initialSkillPoints = characterData ? (characterData.skillPointsRemaining + calculateSpentSkillPoints(characterData.skills)) : 30;
        skillPoints = initialSkillPoints - calculateSpentSkillPoints() + traitPoints;
        updateResourceDisplay();
    }

    function calculateSpentSkillPoints(skills) {
        let spentPoints = 0;
        const skillInputs = skills ? skills : skillsContainer.querySelectorAll('input[type="number"]');

        if (skills) {
            skills.forEach(skill => {
                for (let i = 1; i <= skill.rank; i++) {
                    spentPoints += i;
                }
            });
        } else {
            skillInputs.forEach(input => {
                const rank = parseInt(input.value) || 0;
                for (let i = 1; i <= rank; i++) {
                    spentPoints += i;
                }
            });
        }
        return spentPoints;
    }

    statsContainer.addEventListener('click', function(e) {
        const button = e.target.closest('button');
        if (!button) return;

        const statType = button.dataset.statType;
        const input = document.getElementById(`stat-${statType}`);
        let currentValue = parseInt(input.value);

        if (button.classList.contains('stat-increase')) {
            if (currentValue < parseInt(input.max) && statPoints > 0) {
                input.value = ++currentValue;
                statPoints--;
            }
        } else if (button.classList.contains('stat-decrease')) {
            if (currentValue > parseInt(input.min)) {
                input.value = --currentValue;
                statPoints++;
            }
        }
        updateResourceDisplay();
    });

    skillsContainer.addEventListener('click', function(e) {
        const button = e.target.closest('button');
        if (!button) return;

        const skillId = button.dataset.skillId;
        const input = document.getElementById(`skill-${skillId}`);
        let currentValue = parseInt(input.value);
        
        if (button.classList.contains('skill-increase')) {
            const cost = currentValue + 1;
            if (currentValue < 3 && skillPoints >= cost) {
                input.value = ++currentValue;
                skillPoints -= cost;
            }
        } else if (button.classList.contains('skill-decrease')) {
            if (currentValue > 0) {
                const cost = currentValue;
                input.value = --currentValue;
                skillPoints += cost;
            }
        }
        updateResourceDisplay();
    });

    traitsContainer.addEventListener('change', function(e) {
        if (e.target.type === 'checkbox') {
            recalculateSkillPoints();
        }
    });

    generalItemsContainer.addEventListener('input', function (e) {
        if (e.target.type === 'number') {
            updateResourceDisplay();
            validateResources();
        }
    });

    equipmentContainer.addEventListener('click', function(e) {
        if (e.target.classList.contains('add-to-cart-btn')) {
            const equipId = e.target.dataset.equipId;
            const input = document.getElementById(`equip-${equipId}`);
            const quantityToAdd = parseInt(input.value) || 0;
            
            // Check if quantity is valid
            if (quantityToAdd <= 0) {
                // Show error message
                e.target.textContent = 'Add Quantity First!';
                e.target.style.backgroundColor = '#dc3545';
                setTimeout(() => {
                    e.target.textContent = 'Add to Cart';
                    e.target.style.backgroundColor = '';
                }, 2000);
                return;
            }
            
            // Add to cart
            const currentCartQuantity = cartItems.get(equipId) || 0;
            cartItems.set(equipId, currentCartQuantity + quantityToAdd);
            
            // Reset the input field
            input.value = 0;
            
            // Update the cart display
            const cartInfoSpan = e.target.parentElement.querySelector('.cart-info span');
            if (cartInfoSpan) {
                cartInfoSpan.textContent = `In Cart: ${cartItems.get(equipId)}`;
            }
            
            updateResourceDisplay();
            validateResources();
            
            // Visual feedback
            e.target.textContent = 'Added!';
            e.target.style.backgroundColor = '#28a745';
            setTimeout(() => {
                e.target.textContent = 'Add to Cart';
                e.target.style.backgroundColor = '';
            }, 1000);
        }
    });

    financeChipCheckbox.addEventListener('change', function() {
        updateResourceDisplay();
        validateResources();
    });

    characterForm.addEventListener('submit', function (e) {
        e.preventDefault();

        const allocatedStats = {};
        statsContainer.querySelectorAll('input[type="number"]').forEach(input => {
            allocatedStats[input.dataset.statType] = parseInt(input.value);
        });

        const selectedTraits = [];
        traitsContainer.querySelectorAll('input[type="checkbox"]:checked').forEach(checkbox => {
            selectedTraits.push({
                traitId: checkbox.dataset.traitId,
                rank: 1
            });
        });

        const selectedEquipment = [];
        cartItems.forEach((quantity, equipId) => {
            if (quantity > 0) {
                selectedEquipment.push({
                    equipmentItemId: equipId,
                    quantity: quantity
                });
            }
        });

        const selectedGeneralItems = [];
        generalItemsContainer.querySelectorAll('input[type="number"]').forEach(input => {
            const quantity = parseInt(input.value);
            if (quantity > 0) {
                selectedGeneralItems.push({
                    generalItemId: input.dataset.itemId,
                    quantity: quantity
                });
            }
        });

        const selectedSkills = [];
        skillsContainer.querySelectorAll('input[type="number"]').forEach(input => {
            const rank = parseInt(input.value);
            if (rank > 0) {
                const skillDef = skillsData.find(s => s.id === input.dataset.skillId);
                if (skillDef) {
                    selectedSkills.push({
                        skillName: skillDef.name,
                        rank: rank
                    });
                }
            }
        });

        const updatedCharacterData = {
            id: characterId,
            name: document.getElementById('name').value,
            raceId: raceSelect.value,
            trainingPackageId: trainingPackageSelect.value,
            userId: characterData.userId,
            allocatedStats: allocatedStats,
            selectedSkills: selectedSkills,
            selectedTraits: selectedTraits,
            selectedEquipment: selectedEquipment,
            selectedGeneralItems: selectedGeneralItems,
            chooseFinanceChip: financeChipCheckbox.checked,
            statPointsRemaining: statPoints,
            skillPointsRemaining: skillPoints,
            credits: initialCredits - calculateTotalCost(),
            rowVersion: characterData.rowVersion // Pass the Base64 string.
        };

        fetch(`/api/CharacterApi/${characterId}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(updatedCharacterData),
        })
        .then(async response => {
            if (response.ok) {
                const updatedCharacter = await response.json();
                
                characterData.rowVersion = updatedCharacter.rowVersion || null;

                characterResult.innerHTML = `Character updated successfully! <a href="/CharacterDetails/${characterId}">View Character</a>`;
                characterResult.className = 'success';
            } else if (response.status === 409) { // Concurrency conflict
                const error = await response.json();
                characterResult.textContent = error.message || 'The character was modified by another user. Please reload and try again.';
                characterResult.className = 'error';
            } else {
                const errorText = await response.text();
                throw new Error('Server responded with an error: ' + errorText);
            }
        })
        .catch(error => {
            console.error('Error:', error);
            characterResult.textContent = 'Error updating character: ' + error.message;
            characterResult.className = 'error';
        });
    });
});