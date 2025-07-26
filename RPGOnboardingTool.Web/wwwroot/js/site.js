document.addEventListener('DOMContentLoaded', function () {
    console.log('🚀 Character Creation Script Loading...');

    const raceSelect = document.getElementById('race');
    // If raceSelect is not present, we are not on the character creation page.
    if (!raceSelect) {
        console.log('❌ Not on character creation page - race select not found');
        return; // Exit script if not on the right page
    }

    console.log('✅ Character creation page detected');

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

    // Track items in cart separately from display quantities
    let cartItems = new Map(); // equipmentId -> quantity

    // Memory leak prevention - track event listeners and prevent loops
    let equipmentEventListenersAdded = false;
    let skillsPopulated = false;
    let lastEquipmentFilter = null; // Initialize as null to force first load
    let isPopulatingEquipment = false; // CRITICAL: Prevent infinite loops

    // CRITICAL FIX: Add persistent skill values storage
    let currentSkillValues = new Map(); // skillId -> currentValue

    // Initial values for skill points calculation
    let initialSkillPoints = 30; // Track the original skill points
    let skillPointsSnapshot = null; // Store snapshot before tab switches

    function calculateTotalCost() {
        let totalCost = 0;
        encumbrance = 0; // Reset encumbrance before recalculating

        // Only calculate cost from items actually in cart
        cartItems.forEach((quantity, equipId) => {
            const equipment = equipmentData.find(item => item.id === equipId);
            if (equipment) {
                totalCost += equipment.cost * quantity;
                encumbrance += equipment.weight * quantity;
            }
        });

        // Finance chip ADDS credits instead of costing them
        // No cost calculation for finance chip here since it adds to credits
        return totalCost;
    }

    function updateResourceDisplay() {
        console.log('📊 Updating resource display - StatPoints:', statPoints, 'SkillPoints:', skillPoints);
        statPointsSpan.textContent = statPoints;
        skillPointsSpan.textContent = skillPoints;
        
        // Calculate base credits after equipment costs
        let displayCredits = initialCredits - calculateTotalCost();
        
        // Finance chip ADDS 100 credits to the display
        if (financeChipCheckbox.checked) {
            displayCredits += 100;
        }
        
        creditsSpan.textContent = displayCredits;
        
        const encumbranceSpan = document.getElementById('encumbrance');
        if (encumbranceSpan) {
            encumbranceSpan.textContent = encumbrance.toFixed(2);
        }
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

    // CRITICAL FIX: Function to preserve current skill points before tab switches
    function preserveCurrentSkillValues() {
        console.log('💾 Preserving current skill values...');
        
        // CRITICAL: Store the current skill points before tab switch
        if (skillPointsSnapshot === null) {
            skillPointsSnapshot = skillPoints;
            console.log('📸 Skill points snapshot taken:', skillPointsSnapshot);
        }
        
        skillsContainer.querySelectorAll('input[type="number"]').forEach(input => {
            const skillId = input.dataset.skillId;
            const currentValue = parseInt(input.value) || 0;
            if (currentValue > 0) {
                currentSkillValues.set(skillId, currentValue);
                console.log(`  - Preserved ${skillId}: ${currentValue}`);
            }
        });
    }

    // Fetch initial data
    console.log('🔄 Fetching initial API data...');
    Promise.all([
        fetch('/api/CharacterApi/races').then(res => res.ok ? res.json() : Promise.reject(res)),
        fetch('/api/CharacterApi/training-packages').then(res => res.ok ? res.json() : Promise.reject(res)),
        fetch('/api/CharacterApi/traits').then(res => res.ok ? res.json() : Promise.reject(res)),
        fetch('/api/CharacterApi/equipment').then(res => res.ok ? res.json() : Promise.reject(res)),
        fetch('/api/CharacterApi/skills').then(res => res.ok ? res.json() : Promise.reject(res))
    ].reverse()).then(([skills, equipment, traits, packages, races]) => {
        console.log('✅ API data loaded successfully');
        console.log('📊 Data counts - Races:', races.length, 'Packages:', packages.length, 'Skills:', skills.length, 'Equipment:', equipment.length);

        racesData = races;
        packagesData = packages;
        traitsData = traits;
        equipmentData = equipment;
        skillsData = skills;

        // Log sample skill data to understand the structure
        if (skills.length > 0) {
            console.log('📋 Sample skill data:', skills[0]);
            console.log('📋 All skills stat types:', skills.map(s => ({ name: s.name, stat: s.relatedStat })))
        }

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

        // Ensure race details start hidden
        const raceDetailsContainer = document.getElementById('race-details');
        if (raceDetailsContainer) {
            raceDetailsContainer.style.display = 'none';
            raceDetailsContainer.style.opacity = '0';
            raceDetailsContainer.style.transform = 'translateY(20px)';
            console.log('✅ Race details container initialized as hidden');
        }

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

        // Populate General Items placeholder
        const generalItemsContainer = document.getElementById('general-items-container');
        if (generalItemsContainer) {
            console.log('📝 Populating General Items placeholder...');
            generalItemsContainer.innerHTML = '<p style="color: var(--ps5-gray);">This section will be populated with general items in a future update.</p>';
            console.log('✅ General Items placeholder populated.');
        }

        console.log('✅ Initial population complete');
    }).catch(error => {
        console.error('❌ Error fetching initial data:', error);
        characterResult.textContent = 'Error loading character creation data. Please try again later.';
        characterResult.style.color = 'red';
        characterResult.classList.add('error');
    });

    raceSelect.addEventListener('change', function() {
        console.log('--- Race dropdown clicked ---');
        console.log('🏁 Race changed to:', this.value);
        const selectedRace = racesData.find(r => r.id === this.value);
        const raceDetailsContainer = document.getElementById('race-details');
        const raceImageContainer = document.getElementById('race-image-container');
        const raceImage = document.getElementById('race-image');
        const raceDescription = document.getElementById('race-description');

        // ALWAYS hide the container first
        raceDetailsContainer.style.display = 'none';
        raceDetailsContainer.style.opacity = '0';
        raceDetailsContainer.style.transform = 'translateY(20px)';

        if (raceImageContainer) {
            raceImageContainer.style.display = 'none';
            console.log('🖼️ Image container hidden by default on change.');
        }

        if (selectedRace && this.value !== "" && this.value !== "--Please choose a race--") {
            console.log('✅ Selected race:', selectedRace.name);
            statsSection.style.display = 'block';
            populateStats(selectedRace.statLimits);
            financeChipCheckbox.disabled = !selectedRace.canHaveFinanceChip;
            if (!selectedRace.canHaveFinanceChip) {
                financeChipCheckbox.checked = false;
            }
            // Reset and apply racial skill bonuses
            resetAndApplyRacialSkills(selectedRace.speciesSkills);

            // Show and populate race details
            console.log('🖼️ Setting race image for:', selectedRace.name);
            console.log('🔍 Race image URL from database:', selectedRace.imageUrl);

            // Use the imageUrl from the race data instead of generating it manually
            const primaryImageUrl = selectedRace.imageUrl || `/images/races/${selectedRace.name.toLowerCase().replace(/\s+/g, '-')}.png`;

            // Preload image to check if it exists
            const tempImg = new Image();
            tempImg.onload = function() {
                // Image exists and loaded successfully
                console.log('✅ Race image loaded successfully:', primaryImageUrl);
                logImageDebugInfo(selectedRace.name, primaryImageUrl, true);
                raceImage.src = primaryImageUrl;
                raceImage.alt = selectedRace.name;
                raceImage.style.display = 'block';
                if (raceImageContainer) {
                    raceImageContainer.style.display = 'block';
                    console.log('🖼️ Image container explicitly shown.');
                }

                // Remove any existing placeholder
                const existingPlaceholder = raceDetailsContainer.querySelector('.race-image-placeholder');
                if (existingPlaceholder) {
                    existingPlaceholder.remove();
                }

                // Add click-to-enlarge functionality
                raceImage.onclick = function() {
                    showImageModal(this.src, selectedRace.name);
                };
            };

            tempImg.onerror = function() {
                console.log('⚠️ Primary race image failed to load:', primaryImageUrl);
                logImageDebugInfo(selectedRace.name, primaryImageUrl, false);
                // Try alternative naming patterns as fallback
                const alternatives = [
                    `/images/races/${selectedRace.name.toLowerCase().replace(/\s+/g, '-').replace(/'/g, '').replace(/"/g, '')}.png`,
                    `/images/races/${selectedRace.name.toLowerCase().replace(/\s+/g, '_').replace(/'/g, '').replace(/"/g, '')}.png`,
                    `/images/races/${selectedRace.name.toLowerCase().replace(/[^a-z0-9]/g, '')}.png`,
                    `/images/races/${selectedRace.name.toLowerCase().replace(/[^a-z0-9]/g, '-')}.png`,
                    `/images/placeholder-race.png`
                ];

                console.log('🔄 Trying alternative image patterns:', alternatives);
                tryAlternativeImages(alternatives, 0);
            };

            function tryAlternativeImages(alternatives, index) {
                if (index < alternatives.length) {
                    console.log(`🔄 Trying alternative image ${index + 1}/${alternatives.length}:`, alternatives[index]);
                    const altImg = new Image();
                    altImg.onload = function() {
                        raceImage.src = alternatives[index];
                        raceImage.alt = selectedRace.name;
                        raceImage.style.display = 'block';
                        if (raceImageContainer) {
                            raceImageContainer.style.display = 'block';
                            console.log('🖼️ Image container explicitly shown for alternative image.');
                        }
                        console.log('✅ Successfully loaded alternative image:', alternatives[index]);

                        // Remove placeholder if exists
                        const existingPlaceholder = raceDetailsContainer.querySelector('.race-image-placeholder');
                        if (existingPlaceholder) {
                            existingPlaceholder.remove();
                        }

                        // Add click functionality
                        raceImage.onclick = function() {
                            showImageModal(this.src, selectedRace.name);
                        };
                    };
                    altImg.onerror = function() {
                        console.log(`❌ Alternative image ${index + 1} failed:`, alternatives[index]);
                        tryAlternativeImages(alternatives, index + 1);
                    };
                    altImg.src = alternatives[index];
                } else {
                    // All alternatives failed, show placeholder
                    console.log('❌ All image alternatives failed, creating placeholder for:', selectedRace.name);
                    createRacePlaceholder(selectedRace.name, raceDetailsContainer, raceImage);
                }
            }

            // Start the image loading process with the primary URL
            console.log('🔄 Starting image load test for:', primaryImageUrl);
            tempImg.src = primaryImageUrl;

            // Update race title and description
            const raceTitle = raceDetailsContainer.querySelector('.race-title');
            if (raceTitle) {
                raceTitle.textContent = selectedRace.name;
            }

            raceDescription.textContent = selectedRace.description;

            console.log('🎯 Showing race details container');

            // Show the container with animation after a short delay
            setTimeout(() => {
                raceDetailsContainer.classList.add('show');
                console.log('✅ Race details container display set to flex via "show" class.');

                // Debug: Check if image is visible
                setTimeout(() => {
                    const imageRect = raceImage.getBoundingClientRect();
                    const containerRect = raceDetailsContainer.getBoundingClientRect();
                    const imageContainerRect = raceImageContainer ? raceImageContainer.getBoundingClientRect() : {};

                    console.log('🔍 Final Debug Info:');
                    console.log('  - Race Details Container:', {
                        display: window.getComputedStyle(raceDetailsContainer).display,
                        opacity: window.getComputedStyle(raceDetailsContainer).opacity,
                        visibility: window.getComputedStyle(raceDetailsContainer).visibility,
                        transform: window.getComputedStyle(raceDetailsContainer).transform,
                        rect: containerRect
                    });
                    console.log('  - Image Container:', {
                        display: raceImageContainer ? window.getComputedStyle(raceImageContainer).display : 'N/A',
                        rect: imageContainerRect
                    });
                    console.log('  - Image:', {
                        display: window.getComputedStyle(raceImage).display,
                        opacity: window.getComputedStyle(raceImage).opacity,
                        visibility: window.getComputedStyle(raceImage).visibility,
                        rect: imageRect,
                        src: raceImage.src
                    });
                    console.log('  - Image visible on screen:', imageRect.width > 0 && imageRect.height > 0);

                    // If image is still not visible, force show placeholder
                    if (imageRect.width === 0 || imageRect.height === 0) {
                        console.log('⚠️ Image not visible, forcing placeholder creation');
                        // Force create placeholder since image is not showing
                        createRacePlaceholder(selectedRace.name, raceDetailsContainer, raceImage);
                    }
                }, 550); // Allow time for animation
            }, 100);

        } else {
            console.log('🚫 No race selected or empty value');
            statsSection.style.display = 'none';
            statsContainer.innerHTML = '<p>Please select a race to see stat limits.</p>';
            resetAndApplyRacialSkills([]); // Clear skills if no race is selected
            // Clear race details content and hide container
            const raceTitle = raceDetailsContainer.querySelector('.race-title');
            if (raceTitle) {
                raceTitle.textContent = '';
            }
            raceDescription.textContent = '';
            raceImage.src = '';
            raceImage.alt = '';
            raceImage.style.display = 'none';
            if (raceImageContainer) {
                raceImageContainer.style.display = 'none';
                console.log('🖼️ Image container explicitly hidden for empty selection.');
            }
            raceImage.onerror = null; // Clear error handler
            raceImage.onload = null; // Clear load handler

            // Remove any image placeholders
            const existingPlaceholder = raceDetailsContainer.querySelector('.race-image-placeholder');
            if (existingPlaceholder) {
                existingPlaceholder.remove();
            }

            // Ensure container stays hidden
            raceDetailsContainer.classList.remove('show');
            raceDetailsContainer.style.display = 'none';
            raceDetailsContainer.style.opacity = '0';
            raceDetailsContainer.style.transform = 'translateY(20px)';

            console.log('✅ Race details cleared and hidden');
        }
    });

    trainingPackageSelect.addEventListener('change', function() {
        console.log('--- Training Package dropdown clicked ---');
        console.log('📦 Training package changed to:', this.value);
        const selectedPackage = packagesData.find(p => p.id === this.value);
        console.log('📦 Selected training package:', selectedPackage);

        if (selectedPackage) {
            console.log('✅ Training package selected:', selectedPackage.name);
            skillsSection.style.display = 'block';

            // Ensure skills are populated and visible
            if (!skillsPopulated || skillsContainer.innerHTML === '') {
                console.log('🔄 Populating skills first...');
                populateSkills();
                skillsPopulated = true;
            }

            // Apply package skills
            if (selectedPackage.packageSkills && selectedPackage.packageSkills.length > 0) {
                console.log('✅ Package skills found:', selectedPackage.packageSkills.length, 'skills');
                console.log('📋 Package skills details:', selectedPackage.packageSkills);
                applyPackageSkills(selectedPackage.packageSkills);
            } else {
                console.log('⚠️ No package skills found for this training package');
                // Still apply racial skills if available
                const selectedRace = racesData.find(r => r.id === raceSelect.value);
                if (selectedRace && selectedRace.speciesSkills) {
                    console.log('🔄 Applying racial skills only');
                    resetAndApplyRacialSkills(selectedRace.speciesSkills);
                }
            }
        } else {
            console.log('🚫 No training package selected');
            skillsSection.style.display = 'none';
            const selectedRace = racesData.find(r => r.id === raceSelect.value);
            if (selectedRace) {
                resetAndApplyRacialSkills(selectedRace.speciesSkills || []);
            }
        }
    });

    function populateStats(statLimits) {
        console.log('📊 Populating stats for', statLimits?.length || 0, 'stat limits');
        statsContainer.innerHTML = '';
        if (!statLimits || statLimits.length === 0) {
            statsContainer.innerHTML = '<p>No stat limits defined for this race.</p>';
            return;
        }
        const selectedRace = racesData.find(r => r.id === raceSelect.value);
        statLimits.forEach(limit => {
            const statDiv = document.createElement('div');
            let statName = limit.statType;
            if (selectedRace && selectedRace.name.toLowerCase().includes('ebonite') && statName.toLowerCase() === 'luck') {
                statName = 'FLUX';
            }
            statDiv.innerHTML = `
                <label for="stat-${limit.statType}" title="${getStatDescription(limit.statType)}">${statName}</label>
                <div class="point-controls">
                    <button type="button" class="stat-decrease" data-stat-type="${limit.statType}">-</button>
                    <input type="number" id="stat-${limit.statType}" value="${limit.minValue}" min="${limit.minValue}" max="${limit.maxValue}" data-stat-type="${limit.statType}" readonly>
                    <button type="button" class="stat-increase" data-stat-type="${limit.statType}">+</button>
                </div>
            `;
            statsContainer.appendChild(statDiv);
        });
    }

    function getStatDescription(statType) {
        switch (statType) {
            case 'STR': return 'Strength: Physical power and carrying capacity.';
            case 'DEX': return 'Dexterity: Agility, reflexes, and coordination.';
            case 'KNOW': return 'Knowledge: Intelligence, memory, and reasoning.';
            case 'CONC': return 'Concentration: Willpower, focus, and mental resistance.';
            case 'CHA': return 'Charisma: Social skills, leadership, and persuasion.';
            case 'COOL': return 'Coolness: Composure under pressure and initiative.';
            case 'LUCK': return 'Luck: Chance and fortune.';
            case 'FLUX': return 'Flux: The Ebonite resource for Ebb powers.';
            default: return '';
        }
    }

    function resetAndApplyRacialSkills(racialSkills) {
        console.log('🔄 Resetting and applying racial skills:', racialSkills?.length || 0, 'skills');
        
        // CRITICAL FIX: Clear the stored skill values map when resetting
        currentSkillValues.clear();
        
        // CRITICAL FIX: Clear snapshot when racial skills are reset
        skillPointsSnapshot = null;
        
        // Reset all skill inputs to 0
        skillsContainer.querySelectorAll('input[type="number"]').forEach(input => {
            input.value = "0";
        });

        // Apply racial bonuses
        racialSkills.forEach(racialSkill => {
            console.log('🔍 Looking for racial skill:', racialSkill.skillName);
            const skillDef = findSkillByName(racialSkill.skillName);
            if (skillDef) {
                const input = document.getElementById(`skill-${skillDef.id}`);
                if (input) {
                    console.log(`✅ Applying racial skill: ${racialSkill.skillName} rank ${racialSkill.rank}`);
                    input.value = racialSkill.rank; // Set the base rank from race
                    // Store the racial skill value
                    currentSkillValues.set(skillDef.id, racialSkill.rank);
                } else {
                    console.log(`⚠️ Skill input not found for: ${racialSkill.skillName}`);
                }
            } else {
                console.log(`⚠️ Skill definition not found for: ${racialSkill.skillName}`);
            }
        });
        recalculateSkillPoints();
    }

    function applyPackageSkills(packageSkills) {
        console.log('📦 Applying package skills:', packageSkills?.length || 0, 'skills');
        // First, reset to the racial baseline before applying package skills
        const selectedRace = racesData.find(r => r.id === raceSelect.value);
        if (selectedRace) {
            resetAndApplyRacialSkills(selectedRace.speciesSkills);
        }

        // Add package skills on top of racial skills
        packageSkills.forEach(pkgSkill => {
            console.log('🔍 Looking for package skill:', pkgSkill.skillName);
            const skillDef = findSkillByName(pkgSkill.skillName);
            if (skillDef) {
                const input = document.getElementById(`skill-${skillDef.id}`);
                if (input) {
                    let currentRank = parseInt(input.value) || 0;
                    let newRank = currentRank + pkgSkill.rank;
                    console.log(`✅ Applying package skill: ${pkgSkill.skillName} +${pkgSkill.rank} (${currentRank} -> ${newRank})`);
                    input.value = newRank;
                    // Update stored value
                    currentSkillValues.set(skillDef.id, newRank);
                } else {
                    console.log(`⚠️ Skill input not found for package skill: ${pkgSkill.skillName}`);
                }
            } else {
                console.log(`⚠️ Skill definition not found for package skill: ${pkgSkill.skillName}`);
            }
        });
        
        // CRITICAL FIX: Clear snapshot after applying package skills
        skillPointsSnapshot = null;
        recalculateSkillPoints();
    }

    // Enhanced skill finding function with fuzzy matching
    function findSkillByName(searchName) {
        console.log('🔍 Searching for skill:', searchName);

        // Direct match first
        let skill = skillsData.find(s => s.name === searchName);
        if (skill) {
            console.log('✅ Direct match found:', skill.name);
            return skill;
        }

        // Case-insensitive match
        skill = skillsData.find(s => s.name.toLowerCase() === searchName.toLowerCase());
        if (skill) {
            console.log('✅ Case-insensitive match found:', skill.name);
            return skill;
        }

        // Fuzzy matching for common skill name variations
        const fuzzyMatches = {
            'Survival': ['SURVIVAL'],
            'Language: Gristle': ['LANGUAGE [GRISTLE]'],
            'Lore: Sector': ['LORE [SECTOR]'],
            'Unarmed Combat': ['UNARMED COMBAT'],
            'Intimidate': ['INTIMIDATE'],
            'Athletics': ['ATHLETICS'],
            'Climbing': ['CLIMBING'],
            'Melee/Polearm/Throw or Unarmed': ['MELEE WEAPONS', 'POLEARM', 'THROW', 'UNARMED COMBAT'],
            'Stealth': ['STEALTH'],
            'Acrobatics': ['ACROBATICS'],
            'Education: Academic': ['EDUCATION [ACADEMIC]'],
            'Education: Natural': ['EDUCATION [NATURAL]'],
            'Melee or Unarmed': ['MELEE WEAPONS', 'UNARMED COMBAT'],
            'Drive Civilian': ['DRIVE [CIVILIAN]'],
            'Drive Military': ['DRIVE [MILITARY]'],
            'Medical': ['MEDICAL'],
            'Pistol': ['PISTOL'],
            'Rifle': ['RIFLE'],
            'Computer': ['COMPUTER'],
            'Detect': ['DETECT'],
            'Haggle': ['HAGGLE'],
            'Lockpick (Pick One)': ['LOCK PICK [MANUAL]', 'LOCK PICK [ELECTRONIC]'],
            'Technical (Pick One)': ['TECHNICAL [MECHANICAL]', 'TECHNICAL [ELECTRICAL]', 'TECHNICAL [WEAPONS]'],
            'Interrgate': ['INTERROGATE'], // Fix typo in seed data
            'Streetwise': ['STREETWISE'],
            'Torture': ['TORTURE'],
            'Forensics': ['FORENSICS'],
            'Read Lips': ['READ LIPS'],
            'Tracking': ['TRACKING'],
            'Admin & Finance': ['ADMIN & FINANCE'],
            'Diplomacy': ['DIPLOMACY'],
            'Leadership': ['LEADERSHIP'],
            'Oratory': ['ORATORY'],
            'Bribery': ['BRIBERY'],
            'Language: Neophron': ['LANGUAGE [NEOPHRON]'],
            'Language +1': ['LANGUAGE [WRAITHEN]', 'LANGUAGE [SHAKTARIAN]', 'LANGUAGE [SIGN]'],
            'Persuasion': ['PERSUASION'],
            'Language: Shaktar': ['LANGUAGE [SHAKTARIAN]'],
            'Language: Wraithen': ['LANGUAGE [WRAITHEN]'],
            'Throw': ['THROW'],
            'Demolitions': ['DEMOLITIONS'],
            'Support Weapons': ['SUPPORT WEAPONS'],
            'Tactics': ['TACTICS'],
            'Technical: Weapons': ['TECHNICAL [WEAPONS]']
        };

        if (fuzzyMatches[searchName]) {
            for (const possibleMatch of fuzzyMatches[searchName]) {
                skill = skillsData.find(s => s.name === possibleMatch);
                if (skill) {
                    console.log(`✅ Fuzzy match found: ${searchName} -> ${skill.name}`);
                    return skill;
                }
            }
        }

        // Partial match (contains)
        skill = skillsData.find(s => s.name.toLowerCase().includes(searchName.toLowerCase()) ||
            searchName.toLowerCase().includes(s.name.toLowerCase()));
        if (skill) {
            console.log('✅ Partial match found:', skill.name);
            return skill;
        }

        console.log('❌ No match found for:', searchName);
        return null;
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

        // CRITICAL FIX: Use proper skill points calculation
        // If we have a snapshot (from tab switching), use it as the base
        const baseSkillPoints = skillPointsSnapshot !== null ? skillPointsSnapshot : initialSkillPoints;
        skillPoints = baseSkillPoints - spentPoints + traitPoints;
        
        console.log('🔄 Recalculated skill points - Base:', baseSkillPoints, 'Spent:', spentPoints, 'Trait bonus:', traitPoints, 'Remaining:', skillPoints);
        updateResourceDisplay();
    }

    function populateTraits() {
        console.log('🎭 Populating traits');
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

    // Add event listener for trait filter (only once)
    document.getElementById('trait-filter').addEventListener('change', populateTraits);

    function populateEquipment() {
        // CRITICAL FIX: Prevent recursive calls that cause infinite loops
        if (isPopulatingEquipment) {
            console.log('🚫 Equipment population already in progress, skipping to prevent infinite loop');
            return;
        }

        isPopulatingEquipment = true;
        console.log('⚔️ Populating equipment list...');

        try {
            const container = document.getElementById('equipment-container');
            const searchInput = document.getElementById('equipment-search');
            const categoryFilter = document.getElementById('equipment-category-filter');

            if (!container || !searchInput || !categoryFilter) {
                console.error('❌ Equipment containers not found');
                return;
            }

            // Check if we need to update (prevent unnecessary re-renders)
            const currentFilter = {
                search: searchInput.value.toLowerCase(),
                category: categoryFilter.value
            };

            if (lastEquipmentFilter && JSON.stringify(currentFilter) === JSON.stringify(lastEquipmentFilter)) {
                console.log('⏭️ Equipment filter unchanged, skipping re-render');
                isPopulatingEquipment = false; // Reset flag
                return;
            }

            console.log('  - Initial load or filter change detected. Repopulating.');
            lastEquipmentFilter = currentFilter;
            container.innerHTML = '';

            const searchTerm = currentFilter.search;
            const category = currentFilter.category;

            console.log('🔍 Equipment filter - Category:', category, 'Search:', searchTerm);
            console.log('📊 Total equipment items:', equipmentData.length);

            const filteredEquipment = equipmentData.filter(item => {
                const nameMatch = item.name.toLowerCase().includes(searchTerm);
                // Fix: Handle equipment type properly - compare with string values
                let categoryMatch = true;
                if (category !== 'all') {
                    // Convert enum value to string if it's a number, otherwise use as is
                    const itemType = typeof item.type === 'number' ? 
                        getEquipmentTypeName(item.type) : item.type.toString();
                    categoryMatch = itemType === category;
                }

                return nameMatch && categoryMatch;
            });

            console.log(`✅ Filtered equipment count: ${filteredEquipment.length}`);

            if (filteredEquipment.length === 0) {
                container.innerHTML = '<p>No equipment found matching your criteria.</p>';
                console.log('⚠️ No equipment matched the filter criteria.');
            } else {
                filteredEquipment.forEach(item => {
                    const itemCard = document.createElement('div');
                    itemCard.className = 'equipment-card';
                    // Use placeholder for missing images to prevent 404 errors
                    const imageUrl = item.imageUrl && item.imageUrl !== '/images/placeholder-equipment.png' 
                        ? item.imageUrl 
                        : '/images/placeholder-equipment.png';
                    const cartQuantity = cartItems.get(item.id) || 0;
                    const itemType = typeof item.type === 'number' ? getEquipmentTypeName(item.type) : item.type;
                    
                    const finalDescription = item.description || 'No description available';
                    
                    itemCard.innerHTML = `
                        <img src="${imageUrl}" alt="${item.name}" class="equipment-image" loading="lazy" onerror="this.src='/images/placeholder-equipment.png';">
                        <h4>${item.name}</h4>
                        <p>${finalDescription}</p>
                        <div class="equipment-details">
                            <span>Cost: ${item.cost}c</span>
                            <span>Weight: ${item.weight}</span>
                            <span>Type: ${itemType}</span>
                        </div>
                        <div class="equipment-quantity">
                            <label for="equip-${item.id}">Quantity:</label>
                            <input type="number" id="equip-${item.id}" value="0" min="0" data-equip-id="${item.id}" data-cost="${item.cost}" data-weight="${item.weight}" data-type="${itemType}">
                        </div>
                        <div class="cart-info" style="margin: 5px 0; font-size: 0.9em; color: #666;">
                            <span>In Cart: ${cartQuantity}</span>
                        </div>
                        <button type="button" class="add-to-cart-btn" data-equip-id="${item.id}">Add to Cart</button>
                    `;
                    container.appendChild(itemCard);
                });
            }

        } catch (error) {
            console.error('❌ Error in populateEquipment:', error);
        } finally {
            // CRITICAL: Always reset the flag after a delay to prevent race conditions
            setTimeout(() => {
                isPopulatingEquipment = false;
                console.log('✅ Equipment population completed, flag reset');
            }, 100);
        }
    }

    // Helper function to convert equipment type enum to string
    function getEquipmentTypeName(typeValue) {
        const typeMap = {
            0: 'Weapon',
            1: 'Armor',
            2: 'Ammunition',
            3: 'Gear',
            4: 'Consumable',
            5: 'Utility',
            6: 'Accessory',
            7: 'Miscellaneous'
        };
        return typeMap[typeValue] || 'Unknown';
    }

    // MEMORY LEAK FIX: Add equipment event listeners only once
    if (!equipmentEventListenersAdded) {
        console.log('🔧 Adding equipment event listeners (ONCE)');

        // Debounced search to prevent excessive calls
        let searchTimeout;
        document.getElementById('equipment-search').addEventListener('input', function() {
            clearTimeout(searchTimeout);
            searchTimeout = setTimeout(() => {
                console.log('🔍 Equipment search triggered:', this.value);
                populateEquipment();
            }, 300); // 300ms debounce
        });

        document.getElementById('equipment-search').addEventListener('keydown', function(e) {
            if (e.key === 'Enter') {
                e.preventDefault(); // Prevent form submission
                console.log('🔍 Equipment search Enter key');
                populateEquipment();
            }
        });

        // CRITICAL FIX: Prevent infinite loop with proper debouncing
        let categoryTimeout;
        document.getElementById('equipment-category-filter').addEventListener('change', function() {
            console.log('🔍 Equipment category changed to:', this.value);

            // Clear any existing timeout
            clearTimeout(categoryTimeout);

            // Add delay and check flag to prevent recursive calls
            categoryTimeout = setTimeout(() => {
                if (!isPopulatingEquipment) {
                    console.log('🔄 Triggering equipment population after category change');
                    populateEquipment();
                } else {
                    console.log('🚫 Skipping equipment population - already in progress');
                }
            }, 150); // Longer delay for category changes
        });

        equipmentEventListenersAdded = true;
    }

    function populateSkills() {
        console.log('🎯 Populating skills - Total skills data:', skillsData.length);
        const tabsContainer = document.getElementById('skills-tabs');
        const skillsContainer = document.getElementById('skills-container');

        if (!tabsContainer || !skillsContainer) {
            console.error('❌ Skills containers not found!');
            return;
        }

        tabsContainer.innerHTML = '';
        skillsContainer.innerHTML = '';

        if (skillsData.length === 0) {
            console.error('❌ No skills data available');
            skillsContainer.innerHTML = '<p>No skills data available. Please refresh the page.</p>';
            return;
        }

        const skillGroups = {
            STR: [], DEX: [], KNOW: [], CONC: [], CHA: [], COOL: []
        };

        // Group skills by their related stat with enhanced conversion
        skillsData.forEach(skill => {
            const statName = convertStatToName(skill.relatedStat);
            console.log(`🔄 Processing skill: ${skill.name}, stat: ${skill.relatedStat}, converted: ${statName}`);

            if (skillGroups[statName]) {
                skillGroups[statName].push(skill);
            } else {
                console.warn('⚠️ Unknown stat type for skill:', skill.name, 'stat:', skill.relatedStat, 'converted:', statName);
                // Default to KNOW group for unknown stats
                skillGroups['KNOW'].push(skill);
            }
        });

        console.log('📊 Skills grouped by stat:', Object.keys(skillGroups).map(key => `${key}: ${skillGroups[key].length}`).join(', '));

        // Create tabs for each stat group that has skills
        let activeTabSet = false;
        Object.keys(skillGroups).forEach((group, index) => {
            if (skillGroups[group].length > 0) { // Only create tabs for groups with skills
                const tab = document.createElement('button');
                tab.type = 'button'; // Prevent form submission
                tab.className = 'tab-link';
                tab.textContent = group;
                tab.dataset.group = group;
                if (!activeTabSet) {
                    tab.classList.add('active');
                    activeTabSet = true;
                }
                tabsContainer.appendChild(tab);
                console.log(`✅ Created tab for ${group} with ${skillGroups[group].length} skills`);
            }
        });

        // Add event listener for tab clicks - CRITICAL FIX: Preserve values before switching
        tabsContainer.addEventListener('click', (e) => {
            if (e.target.classList.contains('tab-link')) {
                console.log('🔄 Tab clicked:', e.target.dataset.group);
                
                // CRITICAL FIX: Only preserve values if this is a genuine tab click, not theme switching
                if (e.isTrusted && !e.target.closest('.theme-switcher')) {
                    preserveCurrentSkillValues();
                }
                
                tabsContainer.querySelectorAll('.tab-link').forEach(tab => tab.classList.remove('active'));
                e.target.classList.add('active');
                displaySkillsForGroup(e.target.dataset.group, skillGroups);
            }
        });

        // Display the first group's skills initially
        const firstGroupWithSkills = Object.keys(skillGroups).find(group => skillGroups[group].length > 0);
        if (firstGroupWithSkills) {
            console.log('🎯 Displaying initial skills for group:', firstGroupWithSkills);
            displaySkillsForGroup(firstGroupWithSkills, skillGroups);
        } else {
            console.error('❌ No skill groups have skills!');
            skillsContainer.innerHTML = '<p>No skills available in any category.</p>';
        }

        skillsPopulated = true;
    }

    // Enhanced stat conversion function
    function convertStatToName(statValue) {
        // Handle both numeric and string values
        if (typeof statValue === 'number') {
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

        // Handle string values from the database
        if (typeof statValue === 'string') {
            const stringStatMap = {
                'Strength': 'STR',
                'Dexterity': 'DEX',
                'Knowledge': 'KNOW',
                'Concentration': 'CONC',
                'Charisma': 'CHA',
                'Cool': 'COOL',
                'STR': 'STR',
                'DEX': 'DEX',
                'KNOW': 'KNOW',
                'CONC': 'CONC',
                'CHA': 'CHA',
                'COOL': 'COOL'
            };
            return stringStatMap[statValue] || 'KNOW'; // Default to KNOW for unknown strings
        }

        return 'KNOW'; // Default fallback
    }

    // Helper function to convert stat enum number to name (keeping for compatibility)
    function getStatName(statValue) {
        return convertStatToName(statValue);
    }

    // CRITICAL FIX: Modified displaySkillsForGroup to preserve user inputs
    function displaySkillsForGroup(group, skillGroups) {
        console.log(`🎯 Displaying skills for group: ${group}, count: ${skillGroups[group]?.length || 0}`);
        skillsContainer.innerHTML = '';

        if (!skillGroups[group] || skillGroups[group].length === 0) {
            console.log(`⚠️ No skills available for group: ${group}`);
            skillsContainer.innerHTML = '<p>No skills available for this category.</p>';
            return;
        }

        skillGroups[group].forEach(skill => {
            // CRITICAL FIX: Use stored values if available, otherwise default to 0
            const storedValue = currentSkillValues.get(skill.id) || 0;
            console.log(`  - Skill ${skill.name}: using stored value ${storedValue}`);
            
            const skillDiv = document.createElement('div');
            skillDiv.className = 'skill-item';
            skillDiv.innerHTML = `
                <label for="skill-${skill.id}">${skill.name}</label>
                <div class="point-controls">
                    <button type="button" class="skill-decrease" data-skill-id="${skill.id}">-</button>
                    <input type="number" id="skill-${skill.id}" value="${storedValue}" min="0" max="3" data-skill-id="${skill.id}" readonly>
                    <button type="button" class="skill-increase" data-skill-id="${skill.id}">+</button>
                </div>
            `;
            skillsContainer.appendChild(skillDiv);
        });

        console.log(`✅ Successfully displayed ${skillGroups[group].length} skills for group: ${group}`);
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
                console.log(`📈 Increased ${statType} to ${currentValue}, remaining points: ${statPoints}`);
            }
        } else if (button.classList.contains('stat-decrease')) {
            if (currentValue > parseInt(input.min)) {
                input.value = --currentValue;
                statPoints++;
                console.log(`📉 Decreased ${statType} to ${currentValue}, remaining points: ${statPoints}`);
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
                // CRITICAL FIX: Update stored value when user changes skill
                currentSkillValues.set(skillId, currentValue);
                // CRITICAL FIX: Clear snapshot when user makes actual changes
                skillPointsSnapshot = null;
                console.log(`📈 Increased skill ${skillId} to ${currentValue}, cost: ${cost}, remaining: ${skillPoints}`);
            }
        } else if (button.classList.contains('skill-decrease')) {
            if (currentValue > 0) {
                const cost = currentValue;
                input.value = --currentValue;
                skillPoints += cost;
                // CRITICAL FIX: Update stored value when user changes skill
                currentSkillValues.set(skillId, currentValue);
                // CRITICAL FIX: Clear snapshot when user makes actual changes
                skillPointsSnapshot = null;
                console.log(`📉 Decreased skill ${skillId} to ${currentValue}, refund: ${cost}, remaining: ${skillPoints}`);
            }
        }
        updateResourceDisplay();
    });

    traitsContainer.addEventListener('change', function(e) {
        if (e.target.type === 'checkbox') {
            console.log('🎭 Trait changed:', e.target.dataset.traitId, 'checked:', e.target.checked);
            recalculateSkillPoints();
        }
    });

    equipmentContainer.addEventListener('click', function(e) {
        if (e.target.classList.contains('add-to-cart-btn')) {
            const button = e.target;
            const equipId = button.dataset.equipId;
            const input = document.getElementById(`equip-${equipId}`);
            const quantityToAdd = parseInt(input.value) || 0;
            console.log(`🛒 "Add to Cart" clicked for item ${equipId} with quantity ${quantityToAdd}`);

            if (quantityToAdd <= 0) {
                console.log('⚠️ Invalid quantity. Showing error toast.');
                showToast("Please enter a valid quantity to add.", "error");
                return;
            }

            const item = equipmentData.find(i => i.id === equipId);
            if (!item) {
                console.error(`❌ Equipment with ID ${equipId} not found in equipmentData.`);
                showToast("An error occurred. Could not find the selected item.", "error");
                return;
            }

            const purchaseCost = item.cost * quantityToAdd;
            const availableCredits = initialCredits - calculateTotalCost();
            console.log(`  - Item: ${item.name}, Cost per item: ${item.cost}, Total cost: ${purchaseCost}`);
            console.log(`  - Available credits: ${availableCredits}`);

            if (purchaseCost > availableCredits) {
                console.log('⚠️ Insufficient credits. Showing error toast.');
                showToast("Insufficient credits for this purchase.", "error");
                return;
            }

            // Add item to cart
            const currentQuantityInCart = cartItems.get(equipId) || 0;
            cartItems.set(equipId, currentQuantityInCart + quantityToAdd);
            console.log(`✅ Item added to cart. New quantity for ${item.name}: ${cartItems.get(equipId)}`);

            // Update UI
            updateResourceDisplay();
            validateResources();

            // Update the specific card's "In Cart" display
            const cartInfoSpan = button.closest('.equipment-card').querySelector('.cart-info span');
            if (cartInfoSpan) {
                cartInfoSpan.textContent = `In Cart: ${cartItems.get(equipId)}`;
            }

            button.classList.add('added');
            showToast(`${quantityToAdd} x ${item.name} added to cart.`, "success");
            input.value = 0; // Reset input field

            // Revert button color after a delay
            setTimeout(() => {
                button.classList.remove('added');
            }, 2000);
        }
    });

    financeChipCheckbox.addEventListener('change', function() {
        console.log('💳 Finance chip toggled:', this.checked);
        
        // Show toast message when finance chip is toggled
        if (this.checked) {
            showToast("Finance Chip activated! +100 credits added to your spending power.", "success");
        } else {
            showToast("Finance Chip deactivated. -100 credits removed from spending power.", "info");
        }
        
        updateResourceDisplay();
        validateResources();
    });

    function logImageDebugInfo(raceName, imageUrl, isSuccess) {
        // Enhanced logging for image loading success and error
        console.log(`🖼️ Race ${isSuccess ? 'image loaded' : 'image failed to load'} - ${raceName}`);
        console.log('🔗 Image URL:', imageUrl);
        console.log('📊 Current state:', {
            'statsSectionDisplay': statsSection.style.display,
            'skillsSectionDisplay': skillsSection.style.display,
            'traitsContainerVisible': traitsContainer.offsetWidth > 0,
            'equipmentContainerVisible': equipmentContainer.offsetWidth > 0
        });

        const imageDebugInfo = {
            'Race Name': raceName,
            'Image URL': imageUrl,
            'Load Success': isSuccess,
            'Stats Section Display': statsSection.style.display,
            'Skills Section Display': skillsSection.style.display,
            'Traits Container Visible': traitsContainer.offsetWidth > 0,
            'Equipment Container Visible': equipmentContainer.offsetWidth > 0
        };

        // Log the image debug info as a structured object
        console.table([imageDebugInfo]);
    }

    function showToast(message, type = 'info') {
        console.log(`🍞 Showing toast: "${message}" (Type: ${type})`);
        const toastContainer = document.getElementById('toast-container');
        if (!toastContainer) {
            console.error('❌ Toast container not found! Cannot display toast message.');
            return;
        }

        const toast = document.createElement('div');
        toast.className = `toast toast-${type}`;
        toast.textContent = message;

        toastContainer.appendChild(toast);

        setTimeout(() => {
            toast.classList.add('show');
        }, 100);

        setTimeout(() => {
            toast.classList.remove('show');
            setTimeout(() => {
                if (toast.parentNode === toastContainer) {
                    toastContainer.removeChild(toast);
                }
            }, 500);
        }, 3000);
    }

    characterForm.addEventListener('submit', function (e) {
        e.preventDefault();
        console.log("✅ Character form submitted. Preparing character data...");

        // Validate that required fields are filled
        const characterName = document.getElementById('name').value.trim();
        if (!characterName) {
            showToast("Please enter a character name.", "error");
            return;
        }

        if (!raceSelect.value) {
            showToast("Please select a race.", "error");
            return;
        }

        if (!trainingPackageSelect.value) {
            showToast("Please select a training package.", "error");
            return;
        }

        // Collect allocated stats
        const allocatedStats = {};
        statsContainer.querySelectorAll('input[type="number"]').forEach(input => {
            const statType = input.dataset.statType;
            const value = parseInt(input.value) || 0;
            allocatedStats[statType] = value;
        });

        // Collect selected skills
        const selectedSkills = [];
        skillsContainer.querySelectorAll('input[type="number"]').forEach(input => {
            const skillId = input.dataset.skillId;
            const rank = parseInt(input.value) || 0;
            if (rank > 0) {
                const skillDef = skillsData.find(s => s.id === skillId);
                if (skillDef) {
                    selectedSkills.push({
                        skillName: skillDef.name,
                        rank: rank
                    });
                }
            }
        });

        // Collect selected traits
        const selectedTraits = [];
        traitsContainer.querySelectorAll('input[type="checkbox"]:checked').forEach(checkbox => {
            selectedTraits.push({
                traitId: checkbox.dataset.traitId,
                rank: 1 // Default rank for traits
            });
        });

        // Collect selected equipment
        const selectedEquipment = [];
        cartItems.forEach((quantity, equipmentItemId) => {
            if (quantity > 0) {
                selectedEquipment.push({
                    equipmentItemId: equipmentItemId,
                    quantity: quantity
                });
            }
        });

        // Create character data object
        const characterData = {
            name: characterName,
            userId: "00000000-0000-0000-0000-000000000001", // Temporary hardcoded user ID
            raceId: raceSelect.value,
            trainingPackageId: trainingPackageSelect.value,
            allocatedStats: allocatedStats,
            selectedSkills: selectedSkills,
            selectedTraits: selectedTraits,
            selectedEquipment: selectedEquipment,
            chooseFinanceChip: financeChipCheckbox.checked,
            statPointsRemaining: statPoints,
            skillPointsRemaining: skillPoints,
            credits: initialCredits - calculateTotalCost()
        };

        console.log("📋 Character data prepared:", characterData);

        // Show loading state
        const submitButton = characterForm.querySelector('button[type="submit"]');
        const originalButtonText = submitButton.textContent;
        submitButton.textContent = "Creating Character...";
        submitButton.disabled = true;

        // Make API call to create character
        fetch('/api/CharacterApi', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(characterData)
        })
        .then(async response => {
            if (response.ok) {
                const createdCharacter = await response.json();
                console.log("✅ Character created successfully:", createdCharacter);
                
                // Show success message
                const successMessage = `
                    <div class="success-message">
                        <div class="success-icon">✔️</div>
                        <h2>Character Created Successfully!</h2>
                        <p>Your character, <strong>${characterName}</strong>, has been saved and is ready for adventure!</p>
                        <div style="margin-top: 2rem; display: flex; gap: 1rem; justify-content: center;">
                            <a href="/CharacterList" class="btn">View All Characters</a>
                            <a href="/CharacterDetails/${createdCharacter.id}" class="btn">View Character Details</a>
                        </div>
                    </div>
                `;
                characterResult.innerHTML = successMessage;
                characterForm.style.display = 'none'; // Hide the form
                
                showToast("Character created successfully!", "success");
            } else {
                const errorData = await response.text();
                console.error("❌ Error creating character:", response.status, errorData);
                throw new Error(`Server error (${response.status}): ${errorData}`);
            }
        })
        .catch(error => {
            console.error('❌ Error creating character:', error);
            showToast(`Error creating character: ${error.message}`, "error");
            
            // Show error in result area
            characterResult.innerHTML = `
                <div class="error-message" style="background-color: #ffebee; border: 2px solid #f44336; border-radius: 12px; padding: 2rem; text-align: center; margin-top: 2rem;">
                    <div style="font-size: 48px; color: #f44336;">❌</div>
                    <h2 style="color: #f44336;">Character Creation Failed</h2>
                    <p>There was an error creating your character. Please check the form and try again.</p>
                    <p style="color: #666; font-size: 0.9rem; margin-top: 1rem;">${error.message}</p>
                </div>
            `;
        })
        .finally(() => {
            // Restore button state
            submitButton.textContent = originalButtonText;
            submitButton.disabled = false;
        });
    });

    // MISSING FUNCTION DEFINITIONS - Add these helper functions
    function showImageModal(imageSrc, raceName) {
        console.log('🖼️ Showing image modal for:', raceName);
        
        // Remove existing modal if present
        const existingModal = document.getElementById('image-modal');
        if (existingModal) {
            existingModal.remove();
        }

        // Create modal HTML
        const modal = document.createElement('div');
        modal.id = 'image-modal';
        modal.className = 'image-modal';
        modal.innerHTML = `
            <div class="modal-backdrop"></div>
            <div class="modal-content">
                <button class="modal-close">&times;</button>
                <img src="${imageSrc}" alt="${raceName}" class="modal-image">
                <div class="modal-caption">${raceName}</div>
            </div>
        `;

        document.body.appendChild(modal);

        // Show modal with animation
        setTimeout(() => {
            modal.style.display = 'flex';
            setTimeout(() => {
                modal.classList.add('modal-active');
            }, 10);
        }, 10);

        // Close modal functionality
        function closeModal() {
            modal.classList.remove('modal-active');
            setTimeout(() => {
                modal.style.display = 'none';
                modal.remove();
            }, 300);
        }

        // Event listeners
        modal.querySelector('.modal-close').addEventListener('click', closeModal);
        modal.querySelector('.modal-backdrop').addEventListener('click', closeModal);
        
        // Close with Escape key
        const escapeHandler = (e) => {
            if (e.key === 'Escape') {
                closeModal();
                document.removeEventListener('keydown', escapeHandler);
            }
        };
        document.addEventListener('keydown', escapeHandler);
    }

    function createRacePlaceholder(raceName, container, imageElement) {
        console.log('🖼️ Creating race placeholder for:', raceName);
        
        // Hide the actual image element
        imageElement.style.display = 'none';
        
        // Remove any existing placeholder
        const existingPlaceholder = container.querySelector('.race-image-placeholder');
        if (existingPlaceholder) {
            existingPlaceholder.remove();
        }
        
        // Create placeholder
        const placeholder = document.createElement('div');
        placeholder.className = 'race-image-placeholder';
        placeholder.style.cssText = `
            width: 600px;
            height: 450px;
            background: linear-gradient(135deg, #f0f0f0 0%, #e0e0e0 100%);
            border: 3px solid var(--sla-red);
            border-radius: 20px;
            display: flex;
            flex-direction: column;
            align-items: center;
            justify-content: center;
            color: #666;
            font-size: 1.2rem;
            font-weight: bold;
            text-align: center;
            padding: 2rem;
            box-shadow: 0 20px 40px var(--sla-shadow);
            flex-shrink: 0;
        `;
        
        placeholder.innerHTML = `
            <div style="font-size: 4rem; margin-bottom: 1rem;">🖼️</div>
            <div>Image for "${raceName}"</div>
            <div style="font-size: 0.9rem; margin-top: 0.5rem; opacity: 0.7;">Image not available</div>
        `;
        
        // Insert placeholder before the race description wrapper
        const raceImageContainer = container.querySelector('#race-image-container');
        if (raceImageContainer) {
            raceImageContainer.style.display = 'block';
            raceImageContainer.appendChild(placeholder);
        } else {
            // Fallback: insert at the beginning of the container
            container.insertBefore(placeholder, container.firstChild);
        }
        
        console.log('✅ Race placeholder created successfully');
    }
});