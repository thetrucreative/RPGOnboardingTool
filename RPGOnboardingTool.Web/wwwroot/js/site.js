document.addEventListener('DOMContentLoaded', function () {
    const raceSelect = document.getElementById('race');
    // If raceSelect is not present, we are not on the character creation page.
    if (!raceSelect) {
        return; // Exit script if not on the right page
    }
    const trainingPackageSelect = document.getElementById('trainingPackage');
    const characterForm = document.getElementById('characterForm');
    const characterResult = document.getElementById('characterResult');
    const statsContainer = document.getElementById('stats-container');
    const skillsContainer = document.getElementById('skills-container');
    const statsSection = document.getElementById('stats-section');
    const skillsSection = document.getElementById('skills-section');
    const traitsContainer = document.getElementById('traits-container');
    const equipmentContainer = document.getElementById('equipment-container');
    const statPointsSpan = document.getElementById('statPoints');
    const skillPointsSpan = document.getElementById('skillPoints');
    const creditsSpan = document.getElementById('credits');
    const financeChipCheckbox = document.getElementById('finance-chip');

    let initialCredits = 1500;
    let statPoints = 12;
    let skillPoints = 30;
    let credits = 1500;
    let racesData = [];
    let packagesData = [];
    let traitsData = [];
    let equipmentData = [];
    let skillsData = [];
    let encumbrance = 0;

    function updateResourceDisplay() {
        statPointsSpan.textContent = statPoints;
        skillPointsSpan.textContent = skillPoints;
        creditsSpan.textContent = initialCredits - calculateTotalCost();
        const encumbranceSpan = document.getElementById('encumbrance');
        if (encumbranceSpan) {
            encumbranceSpan.textContent = encumbrance.toFixed(2);
        }
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
            totalCost += 100; // Cost of finance chip
        }
        return totalCost;
    }

    function validateResources() {
        const remainingCredits = initialCredits - calculateTotalCost();
        const submitButton = characterForm.querySelector('button[type="submit"]');

        if (remainingCredits < 0) {
            creditsSpan.parentElement.style.color = 'red';
            submitButton.disabled = true;
            submitButton.style.backgroundColor = '#6c757d';
            submitButton.title = "You have exceeded your credit limit.";
        } else {
            creditsSpan.parentElement.style.color = ''; // Revert to default color
            submitButton.disabled = false;
            submitButton.style.backgroundColor = '';
            submitButton.title = "";
        }
    }

    // Fetch initial data
    Promise.all([
        fetch('/api/CharacterApi/races').then(res => res.ok ? res.json() : Promise.reject(res)),
        fetch('/api/CharacterApi/training-packages').then(res => res.ok ? res.json() : Promise.reject(res)),
        fetch('/api/CharacterApi/traits').then(res => res.ok ? res.json() : Promise.reject(res)),
        fetch('/api/CharacterApi/equipment').then(res => res.ok ? res.json() : Promise.reject(res)),
        fetch('/api/CharacterApi/skills').then(res => res.ok ? res.json() : Promise.reject(res))
    ]).then(([races, packages, traits, equipment, skills]) => {
        racesData = races;
        packagesData = packages;
        traitsData = traits;
        equipmentData = equipment;
        skillsData = skills;

        const defaultRaceOption = document.createElement('option');
        defaultRaceOption.value = "";
        defaultRaceOption.textContent = "Select a Race";
        raceSelect.appendChild(defaultRaceOption);

        races.forEach(race => {
            const option = document.createElement('option');
            option.value = race.id;
            option.textContent = race.name;
            raceSelect.appendChild(option);
        });

        const defaultPackageOption = document.createElement('option');
        defaultPackageOption.value = "";
        defaultPackageOption.textContent = "Select a Training Package";
        trainingPackageSelect.appendChild(defaultPackageOption);

        packages.forEach(pkg => {
            const option = document.createElement('option');
            option.value = pkg.id;
            option.textContent = pkg.name;
            trainingPackageSelect.appendChild(option);
        });

        populateTraits();
        populateEquipment();
        populateSkills();
        updateResourceDisplay(); // Initial display update
        validateResources(); // Initial validation
    }).catch(error => {
        console.error('Error fetching initial data:', error);
        characterResult.textContent = 'Error loading character creation data. Please try again later.';
        characterResult.style.color = 'red';
        characterResult.classList.add('error');
    });

    raceSelect.addEventListener('change', function() {
        const selectedRace = racesData.find(r => r.id === this.value);
        if (selectedRace) {
            statsSection.style.display = 'block';
            populateStats(selectedRace.statLimits);
            financeChipCheckbox.disabled = !selectedRace.canHaveFinanceChip;
            if (!selectedRace.canHaveFinanceChip) {
                financeChipCheckbox.checked = false;
            }
            // Reset and apply racial skill bonuses
            resetAndApplyRacialSkills(selectedRace.speciesSkills);
        } else {
            statsSection.style.display = 'none';
            statsContainer.innerHTML = '<p>Please select a race to see stat limits.</p>';
            resetAndApplyRacialSkills([]); // Clear skills if no race is selected
        }
    });

    trainingPackageSelect.addEventListener('change', function() {
        const selectedPackage = packagesData.find(p => p.id === this.value);
        if (selectedPackage) {
            skillsSection.style.display = 'block';
            applyPackageSkills(selectedPackage.packageSkills);
        } else {
            skillsSection.style.display = 'none';
            // If you want to reset skills when a package is deselected, call the race change handler.
            raceSelect.dispatchEvent(new Event('change'));
        }
    });

    function populateStats(statLimits) {
        statsContainer.innerHTML = '';
        if (!statLimits || statLimits.length === 0) {
            statsContainer.innerHTML = '<p>No stat limits defined for this race.</p>';
            return;
        }
        statLimits.forEach(limit => {
            const statDiv = document.createElement('div');
            statDiv.innerHTML = `
                <label for="stat-${limit.statType}">${limit.statType}</label>
                <div class="point-controls">
                    <button type="button" class="stat-decrease" data-stat-type="${limit.statType}">-</button>
                    <input type="number" id="stat-${limit.statType}" value="${limit.minValue}" min="${limit.minValue}" max="${limit.maxValue}" data-stat-type="${limit.statType}" readonly>
                    <button type="button" class="stat-increase" data-stat-type="${limit.statType}">+</button>
                </div>
            `;
            statsContainer.appendChild(statDiv);
        });
    }

    function resetAndApplyRacialSkills(racialSkills) {
        // Reset all skill inputs to 0
        skillsContainer.querySelectorAll('input[type="number"]').forEach(input => {
            input.value = "0";
        });

        // Apply racial bonuses
        racialSkills.forEach(racialSkill => {
            const skillDef = skillsData.find(s => s.name === racialSkill.skillName);
            if (skillDef) {
                const input = document.getElementById(`skill-${skillDef.id}`);
                if (input) {
                    input.value = racialSkill.rank; // Set the base rank from race
                }
            }
        });
        recalculateSkillPoints();
    }

    function applyPackageSkills(packageSkills) {
        // First, reset to the racial baseline before applying package skills
        const selectedRace = racesData.find(r => r.id === raceSelect.value);
        if (selectedRace) {
            resetAndApplyRacialSkills(selectedRace.speciesSkills);
        }

        // Assumes racial skills are already applied. This adds package skills on top.
        packageSkills.forEach(pkgSkill => {
            const skillDef = skillsData.find(s => s.name === pkgSkill.skillName);
            if (skillDef) {
                const input = document.getElementById(`skill-${skillDef.id}`);
                if (input) {
                    let currentRank = parseInt(input.value) || 0;
                    input.value = currentRank + pkgSkill.rank; // Add package rank
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

        skillPoints = 30 - spentPoints + traitPoints;
        updateResourceDisplay();
    }


    function populateTraits() {
        traitsContainer.innerHTML = '';
        const filter = document.getElementById('trait-filter').value;

        traitsData
            .filter(trait => filter === 'all' || trait.type.toLowerCase() === filter)
            .forEach(trait => {
                const traitDiv = document.createElement('div');
                traitDiv.className = 'trait';
                traitDiv.innerHTML = `
                    <input type="checkbox" id="trait-${trait.id}" data-trait-id="${trait.id}" data-point-cost="${trait.basePointCost}">
                    <label for="trait-${trait.id}">${trait.name} (${trait.basePointCost} points)</label>
                    <p>${trait.description}</p>
                `;
                traitsContainer.appendChild(traitDiv);
            });
    }

    document.getElementById('trait-filter').addEventListener('change', populateTraits);

    function populateEquipment() {
        equipmentContainer.innerHTML = '';
        equipmentData.forEach(item => {
            const itemDiv = document.createElement('div');
            itemDiv.className = 'form-group';
            itemDiv.innerHTML = `
                <label for="equip-${item.id}">${item.name} (${item.cost} credits)</label>
                <input type="number" id="equip-${item.id}" value="0" min="0" data-equip-id="${item.id}" data-cost="${item.cost}" data-weight="${item.weight}">
            `;
            equipmentContainer.appendChild(itemDiv);
        });
    }

    function populateSkills() {
        skillsContainer.innerHTML = '';
        skillsData.forEach(skill => {
            const skillDiv = document.createElement('div');
            skillDiv.innerHTML = `
                <label for="skill-${skill.id}">${skill.name} (Stat: ${skill.relatedStat})</label>
                <div class="point-controls">
                    <button type="button" class="skill-decrease" data-skill-id="${skill.id}">-</button>
                    <input type="number" id="skill-${skill.id}" value="0" min="0" max="3" data-skill-id="${skill.id}" readonly>
                    <button type="button" class="skill-increase" data-skill-id="${skill.id}">+</button>
                </div>
            `;
            skillsContainer.appendChild(skillDiv);
        });
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

    // Handle form submission
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
                rank: 1 // Assuming rank 1 for now
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
                selectedSkills.push({
                    skillName: skillsData.find(s => s.id === input.dataset.skillId).name,
                    rank: rank
                });
            }
        });

        const characterData = {
            name: document.getElementById('name').value,
            raceId: raceSelect.value,
            trainingPackageId: trainingPackageSelect.value,
            userId: '00000000-0000-0000-0000-000000000001', // Example User ID
            allocatedStats: allocatedStats,
            selectedSkills: selectedSkills, 
            selectedTraits: selectedTraits,
            selectedEquipment: selectedEquipment,
            chooseFinanceChip: financeChipCheckbox.checked
        };

        fetch('/api/CharacterApi', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(characterData),
        })
        .then(response => {
            if (response.ok) {
                return response.json();
            }
            return response.text().then(text => { throw new Error('Server responded with an error: ' + text) });
        })
        .then(data => {
            if (data.id) {
                characterResult.innerHTML = `Character "${data.name}" created successfully! <a href="/CharacterDetails/${data.id}">View Character</a>`;
                characterResult.className = 'success';
            } else {
                characterResult.textContent = 'Error creating character: ' + (data.message || 'Unknown error');
                characterResult.className = 'error';
            }
        })
        .catch(error => {
            console.error('Error:', error);
            characterResult.classList.remove('success');
            characterResult.classList.add('error');
            characterResult.textContent = 'Error creating character: ' + error.message;
        });
    });
});