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
    const statPointsSpan = document.getElementById('statPoints');
    const skillPointsSpan = document.getElementById('skillPoints');
    const creditsSpan = document.getElementById('credits');
    const financeChipCheckbox = document.getElementById('finance-chip');

    let racesData = [];
    let packagesData = [];
    let traitsData = [];
    let equipmentData = [];
    let skillsData = [];
    let characterData = {};

    let statPoints = 0;
    let skillPoints = 0;
    let initialCredits = 0;
    let encumbrance = 0;

    // Fetch all necessary data
    Promise.all([
        fetch('/api/CharacterApi/races').then(res => res.json()),
        fetch('/api/CharacterApi/training-packages').then(res => res.json()),
        fetch('/api/CharacterApi/traits').then(res => res.json()),
        fetch('/api/CharacterApi/equipment').then(res => res.json()),
        fetch('/api/CharacterApi/skills').then(res => res.json()),
        fetch(`/api/CharacterApi/${characterId}`)
    ]).then(async ([races, packages, traits, equipment, skills, characterResponse]) => {
        racesData = races;
        packagesData = packages;
        traitsData = traits;
        equipmentData = equipment;
        skillsData = skills;
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
        skillsContainer.innerHTML = '';
        skillsData.forEach(skill => {
            const existingSkill = existingSkills.find(s => s.name === skill.name);
            const rank = existingSkill ? existingSkill.rank : 0;
            const skillDiv = document.createElement('div');
            skillDiv.innerHTML = `
                <label>${skill.name} (Stat: ${skill.relatedStat})</label>
                <button type="button" class="skill-decrease" data-skill-id="${skill.id}">-</button>
                <input type="number" id="skill-${skill.id}" value="${rank}" min="0" max="3" data-skill-id="${skill.id}" readonly>
                <button type="button" class="skill-increase" data-skill-id="${skill.id}">+</button>
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

    function populateEquipment(existingEquipment = []) {
        equipmentContainer.innerHTML = '';
        equipmentData.forEach(item => {
            const existingItem = existingEquipment.find(e => e.equipmentItemId === item.id);
            const quantity = existingItem ? existingItem.quantity : 0;
            const itemDiv = document.createElement('div');
            itemDiv.className = 'form-group';
            itemDiv.innerHTML = `
                <label for="equip-${item.id}">${item.name} (${item.cost} credits)</label>
                <input type="number" id="equip-${item.id}" value="${quantity}" min="0" data-equip-id="${item.id}" data-cost="${item.cost}" data-weight="${item.weight}">
            `;
            equipmentContainer.appendChild(itemDiv);
        });
    }

    //adapted from site.js
    function updateResourceDisplay() {
        statPointsSpan.textContent = statPoints;
        skillPointsSpan.textContent = skillPoints;
        creditsSpan.textContent = initialCredits - calculateTotalCost();
        document.getElementById('encumbrance').textContent = encumbrance;
    }

    function calculateTotalCost() {
        let totalCost = 0;
        encumbrance = 0; // Reset encumbrance before recalculating
        equipmentContainer.querySelectorAll('input[type="number"]').forEach(input => {
            const quantity = parseInt(input.value) || 0;
            const cost = parseInt(input.dataset.cost) || 0;
            const weight = parseFloat(input.dataset.weight) || 0;
            totalCost += quantity * cost;
            encumbrance += quantity * weight;
        });

        if (financeChipCheckbox.checked) {
            totalCost += 100;
        }
        return totalCost;
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

    equipmentContainer.addEventListener('input', function(e) {
        if (e.target.type === 'number') {
            updateResourceDisplay();
            validateResources();
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
        equipmentContainer.querySelectorAll('input[type="number"]').forEach(input => {
            const quantity = parseInt(input.value);
            if (quantity > 0) {
                selectedEquipment.push({
                    equipmentItemId: input.dataset.equipId,
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