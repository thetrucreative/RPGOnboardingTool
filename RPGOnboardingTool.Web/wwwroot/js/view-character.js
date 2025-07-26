document.addEventListener('DOMContentLoaded', function () {
    const characterId = window.location.pathname.split('/').pop();
    const characterNameEl = document.getElementById('character-name');
    const characterNameDisplayEl = document.getElementById('character-name-display');
    const characterDetailsContainer = document.querySelector('.character-sheet-modern');
    const equipmentContainer = document.getElementById('equipment-container');
    const editCharacterBtn = document.getElementById('edit-character-btn');
    const editCharacterBtnSidebar = document.getElementById('edit-character-btn-sidebar');
    const loadingSpinner = document.getElementById('loading-spinner');
    const errorMessage = document.getElementById('error-message');

    // PDF and Print functionality
    const downloadPdfBtn = document.getElementById('download-pdf-btn');
    const downloadPdfBtn2 = document.getElementById('download-pdf-btn-2');
    const printBtn = document.getElementById('print-btn');
    const printBtn2 = document.getElementById('print-btn-2');

    // Track failed images to prevent repeated requests
    const failedImages = new Set();
    
    // Cache for API requests
    let characterDataCache = null;
    let isLoading = false;

    if (characterId && !isLoading) {
        const editUrl = `/EditCharacter/${characterId}`;
        editCharacterBtn.href = editUrl;
        if (editCharacterBtnSidebar) editCharacterBtnSidebar.href = editUrl;
        
        loadCharacterData();
    }

    function loadCharacterData() {
        if (isLoading || characterDataCache) return;
        
        isLoading = true;
        showLoading();
        
        fetch(`/api/CharacterApi/${characterId}`)
            .then(response => {
                if (!response.ok) {
                    throw new Error(`HTTP ${response.status}: ${response.statusText}`);
                }
                return response.json();
            })
            .then(character => {
                characterDataCache = character;
                isLoading = false;
                hideLoading();
                displayCharacter(character);
                showCharacterSheet();
                addInteractiveFeatures();
                setupPdfAndPrintHandlers(character);
            })
            .catch(error => {
                console.error('Error fetching character:', error);
                isLoading = false;
                hideLoading();
                showError();
            });
    }

    function setupPdfAndPrintHandlers(character) {
        // PDF Download handlers
        [downloadPdfBtn, downloadPdfBtn2].forEach(btn => {
            if (btn) {
                btn.addEventListener('click', () => downloadPdf(character));
            }
        });

        // Print handlers
        [printBtn, printBtn2].forEach(btn => {
            if (btn) {
                btn.addEventListener('click', () => printCharacterSheet());
            }
        });
    }

    function downloadPdf(character) {
        console.log('📄 Starting PDF download for character:', character.name);
        
        try {
            // Show loading state
            const downloadBtns = [downloadPdfBtn, downloadPdfBtn2].filter(btn => btn);
            downloadBtns.forEach(btn => {
                btn.textContent = 'Generating PDF...';
                btn.disabled = true;
            });

            // Create a clean version of the character sheet for PDF
            const pdfContent = createPdfContent(character);
            
            // Use browser's built-in print functionality with PDF styling
            const printWindow = window.open('', '_blank');
            
            const printDocument = `
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset="utf-8">
                    <title>${character.name} - Character Sheet</title>
                    <style>
                        ${getPdfStyles()}
                    </style>
                </head>
                <body>
                    <div class="pdf-container">
                        ${pdfContent}
                    </div>
                    <script>
                        window.onload = function() {
                            setTimeout(() => {
                                window.print();
                                setTimeout(() => window.close(), 1000);
                            }, 500);
                        };
                    </script>
                </body>
                </html>
            `;
            
            printWindow.document.write(printDocument);
            printWindow.document.close();
            
            // Reset button states
            setTimeout(() => {
                downloadBtns.forEach(btn => {
                    btn.textContent = '📄 Download PDF';
                    btn.disabled = false;
                });
            }, 2000);
            
        } catch (error) {
            console.error('❌ Error generating PDF:', error);
            
            // Show error and reset buttons
            downloadBtns.forEach(btn => {
                btn.textContent = 'PDF Error - Try Again';
                btn.style.backgroundColor = '#dc3545';
                btn.disabled = false;
                
                setTimeout(() => {
                    btn.textContent = '📄 Download PDF';
                    btn.style.backgroundColor = '';
                }, 3000);
            });
        }
    }

    function createPdfContent(character) {
        const formatDate = (dateString) => {
            if (!dateString) return 'Unknown';
            return new Date(dateString).toLocaleDateString();
        };

        const formatEquipment = (equipment) => {
            if (!equipment || equipment.length === 0) {
                return '<p class="no-data">No equipment</p>';
            }
            
            return equipment.map(item => `
                <div class="pdf-equipment-item">
                    <div class="equipment-header">
                        <span class="equipment-name">${item.equipmentItem?.name || 'Unknown Item'}</span>
                        <span class="equipment-quantity">×${item.quantity}</span>
                    </div>
                    <div class="equipment-details">
                        <span>Cost: ${item.equipmentItem?.cost || 0}c each</span>
                        <span>Weight: ${item.equipmentItem?.weight || 0} each</span>
                        <span>Total Weight: ${(item.equipmentItem?.weight || 0) * item.quantity}</span>
                    </div>
                </div>
            `).join('');
        };

        const formatSkills = (skills) => {
            if (!skills || skills.length === 0) {
                return '<p class="no-data">No skills</p>';
            }
            
            // Group skills by stat type for better organization
            const groupedSkills = skills.reduce((groups, skill) => {
                const statName = getStatName(skill.relatedStat);
                if (!groups[statName]) groups[statName] = [];
                groups[statName].push(skill);
                return groups;
            }, {});

            return Object.entries(groupedSkills).map(([statName, skillList]) => `
                <div class="pdf-skill-group">
                    <h4 class="skill-group-title">${statName} Skills</h4>
                    <div class="skills-list">
                        ${skillList.map(skill => `
                            <div class="pdf-skill-item">
                                <span class="skill-name">${skill.name}</span>
                                <span class="skill-rank">Rank ${skill.rank}</span>
                            </div>
                        `).join('')}
                    </div>
                </div>
            `).join('');
        };

        const formatTraits = (traits) => {
            if (!traits || traits.length === 0) {
                return '<p class="no-data">No traits</p>';
            }
            
            return traits.map(trait => `
                <div class="pdf-trait-item">
                    <div class="trait-header">
                        <span class="trait-name">${trait.trait?.name || 'Unknown Trait'}</span>
                        <span class="trait-cost">${trait.trait?.basePointCost || 0} points</span>
                    </div>
                    <p class="trait-description">${trait.trait?.description || 'No description'}</p>
                </div>
            `).join('');
        };

        // Helper function to get stat name
        function getStatName(statValue) {
            // Enhanced function to handle both string and numeric stat types
            if (typeof statValue === 'string') {
                return statValue.toUpperCase();
            }
            
            const statMap = {
                0: 'STR', 1: 'DEX', 2: 'KNOW', 
                3: 'CONC', 4: 'CHA', 5: 'COOL'
            };
            return statMap[statValue] || 'MISC'; // Changed from 'UNKNOWN' to 'MISC'
        }

        return `
            <div class="pdf-header">
                <h1 class="character-name">${character.name}</h1>
                <div class="character-subtitle">
                    <span>Race: ${character.characterRace?.name || 'Unknown'}</span>
                    <span>•</span>
                    <span>Package: ${character.characterTrainingPackage?.name || 'None'}</span>
                    <span>•</span>
                    <span>Generated: ${formatDate(new Date().toISOString())}</span>
                </div>
            </div>

            <div class="pdf-grid">
                <!-- Basic Information -->
                <div class="pdf-section">
                    <h2>Basic Information</h2>
                    <div class="info-grid">
                        <div class="info-item">
                            <span class="label">SCL:</span>
                            <span class="value">${character.scl || 0}</span>
                        </div>
                        <div class="info-item">
                            <span class="label">Unis:</span>
                            <span class="value">${character.unis || 0}</span>
                        </div>
                        <div class="info-item">
                            <span class="label">Credits:</span>
                            <span class="value">${character.credits || 0}</span>
                        </div>
                        <div class="info-item">
                            <span class="label">Finance Chip:</span>
                            <span class="value">${character.hasFinanceChip ? 'Yes' : 'No'}</span>
                        </div>
                    </div>
                </div>

                <!-- Stats -->
                <div class="pdf-section">
                    <h2>Character Stats</h2>
                    <div class="stats-grid">
                        ${character.stats ? character.stats.map(stat => `
                            <div class="stat-item">
                                <span class="stat-name">${getStatName(stat.type)}</span>
                                <span class="stat-value">${stat.value}</span>
                            </div>
                        `).join('') : '<p class="no-data">No stats available</p>'}
                    </div>
                </div>

                <!-- Skills -->
                <div class="pdf-section full-width">
                    <h2>Skills & Abilities</h2>
                    <div class="skills-container">
                        ${formatSkills(character.skills)}
                    </div>
                </div>

                <!-- Traits -->
                <div class="pdf-section">
                    <h2>Traits</h2>
                    <div class="traits-container">
                        ${formatTraits(character.characterTraits)}
                    </div>
                </div>

                <!-- Equipment -->
                <div class="pdf-section">
                    <h2>Equipment</h2>
                    <div class="equipment-container">
                        ${formatEquipment(character.characterEquipment)}
                    </div>
                </div>
            </div>

            <div class="pdf-footer">
                <p>Character sheet generated on ${formatDate(new Date().toISOString())} | SLA INDUSTRIES OPERATIVES DOSSIER</p>
            </div>
        `;
    }

    function getPdfStyles() {
        return `
            * {
                margin: 0;
                padding: 0;
                box-sizing: border-box;
            }

            body {
                font-family: Arial, sans-serif;
                font-size: 12px;
                line-height: 1.4;
                color: #333;
                background: white;
            }

            .pdf-container {
                max-width: 210mm;
                margin: 0 auto;
                padding: 20mm;
                background: white;
            }

            .pdf-header {
                text-align: center;
                margin-bottom: 30px;
                padding-bottom: 20px;
                border-bottom: 3px solid #007bff;
            }

            .character-name {
                font-size: 28px;
                font-weight: bold;
                color: #007bff;
                margin-bottom: 10px;
            }

            .character-subtitle {
                font-size: 14px;
                color: #666;
            }

            .character-subtitle span {
                margin: 0 5px;
            }

            .pdf-grid {
                display: grid;
                grid-template-columns: 1fr 1fr;
                gap: 20px;
                margin-bottom: 30px;
            }

            .pdf-section {
                background: #f8f9fa;
                border: 1px solid #dee2e6;
                border-radius: 8px;
                padding: 15px;
                break-inside: avoid;
            }

            .pdf-section.full-width {
                grid-column: 1 / -1;
            }

            .pdf-section h2 {
                font-size: 16px;
                color: #007bff;
                margin-bottom: 15px;
                padding-bottom: 5px;
                border-bottom: 1px solid #dee2e6;
            }

            .info-grid {
                display: grid;
                grid-template-columns: 1fr 1fr;
                gap: 10px;
            }

            .info-item {
                display: flex;
                justify-content: space-between;
                padding: 8px;
                background: white;
                border-radius: 4px;
                border: 1px solid #e9ecef;
            }

            .label {
                font-weight: bold;
                color: #495057;
            }

            .value {
                color: #007bff;
                font-weight: bold;
            }

            .stats-grid {
                display: grid;
                grid-template-columns: repeat(3, 1fr);
                gap: 10px;
            }

            .stat-item {
                text-align: center;
                padding: 10px;
                background: white;
                border-radius: 4px;
                border: 1px solid #e9ecef;
            }

            .stat-name {
                display: block;
                font-size: 10px;
                color: #6c757d;
                font-weight: bold;
                text-transform: uppercase;
                margin-bottom: 5px;
            }

            .stat-value {
                display: block;
                font-size: 18px;
                font-weight: bold;
                color: #007bff;
            }

            .pdf-skill-group {
                margin-bottom: 15px;
            }

            .skill-group-title {
                font-size: 14px;
                color: #495057;
                font-weight: bold;
                margin-bottom: 8px;
                padding: 5px 0;
                border-bottom: 1px solid #dee2e6;
            }

            .skills-list {
                display: grid;
                grid-template-columns: repeat(2, 1fr);
                gap: 5px;
            }

            .pdf-skill-item {
                display: flex;
                justify-content: space-between;
                align-items: center;
                padding: 5px 8px;
                background: white;
                border-radius: 3px;
                border: 1px solid #e9ecef;
            }

            .skill-name {
                font-size: 11px;
                color: #495057;
            }

            .skill-rank {
                background: #007bff;
                color: white;
                padding: 2px 6px;
                border-radius: 10px;
                font-size: 10px;
                font-weight: bold;
            }

            .pdf-trait-item {
                margin-bottom: 10px;
                padding: 10px;
                background: white;
                border-radius: 4px;
                border: 1px solid #e9ecef;
            }

            .trait-header {
                display: flex;
                justify-content: space-between;
                align-items: center;
                margin-bottom: 5px;
            }

            .trait-name {
                font-weight: bold;
                color: #495057;
            }

            .trait-cost {
                background: #6c757d;
                color: white;
                padding: 2px 6px;
                border-radius: 3px;
                font-size: 10px;
                font-weight: bold;
            }

            .trait-description {
                font-size: 11px;
                color: #6c757d;
                line-height: 1.3;
            }

            .pdf-equipment-item {
                margin-bottom: 10px;
                padding: 10px;
                background: white;
                border-radius: 4px;
                border: 1px solid #e9ecef;
            }

            .equipment-header {
                display: flex;
                justify-content: space-between;
                align-items: center;
                margin-bottom: 5px;
            }

            .equipment-name {
                font-weight: bold;
                color: #495057;
            }

            .equipment-quantity {
                background: #28a745;
                color: white;
                padding: 2px 6px;
                border-radius: 3px;
                font-size: 10px;
                font-weight: bold;
            }

            .equipment-details {
                font-size: 10px;
                color: #6c757d;
                display: flex;
                gap: 15px;
                flex-wrap: wrap;
            }

            .no-data {
                text-align: center;
                color: #6c757d;
                font-style: italic;
                padding: 20px;
                background: #f8f9fa;
                border-radius: 4px;
                border: 1px dashed #dee2e6;
            }

            .pdf-footer {
                margin-top: 30px;
                padding-top: 20px;
                border-top: 1px solid #dee2e6;
                text-align: center;
                color: #6c757d;
                font-size: 10px;
            }

            /* Print-specific styles */
            @media print {
                body {
                    -webkit-print-color-adjust: exact;
                    print-color-adjust: exact;
                }
                
                .pdf-container {
                    margin: 0;
                    padding: 10mm;
                    max-width: none;
                }
                
                .pdf-section {
                    break-inside: avoid;
                    page-break-inside: avoid;
                }
                
                .pdf-grid {
                    page-break-inside: avoid;
                }
            }

            /* Responsive adjustments for smaller screens */
            @media (max-width: 768px) {
                .pdf-grid {
                    grid-template-columns: 1fr;
                }
                
                .stats-grid {
                    grid-template-columns: repeat(2, 1fr);
                }
                
                .skills-list {
                    grid-template-columns: 1fr;
                }
            }
        `;
    }

    function printCharacterSheet() {
        console.log('🖨️ Starting print for character sheet');
        
        try {
            // Use the browser's built-in print functionality
            window.print();
        } catch (error) {
            console.error('❌ Error printing character sheet:', error);
            
            // Show error message
            const printBtns = [printBtn, printBtn2].filter(btn => btn);
            printBtns.forEach(btn => {
                const originalText = btn.textContent;
                btn.textContent = 'Print Error - Try Again';
                btn.style.backgroundColor = '#dc3545';
                
                setTimeout(() => {
                    btn.textContent = originalText;
                    btn.style.backgroundColor = '';
                }, 3000);
            });
        }
    }

    // Utility functions for character display
    function showLoading() {
        if (loadingSpinner) loadingSpinner.style.display = 'block';
        if (characterDetailsContainer) characterDetailsContainer.style.display = 'none';
        if (errorMessage) errorMessage.style.display = 'none';
    }

    function hideLoading() {
        if (loadingSpinner) loadingSpinner.style.display = 'none';
    }

    function showError() {
        if (errorMessage) errorMessage.style.display = 'block';
        if (characterDetailsContainer) characterDetailsContainer.style.display = 'none';
        if (loadingSpinner) loadingSpinner.style.display = 'none';
    }

    function showCharacterSheet() {
        if (characterDetailsContainer) characterDetailsContainer.style.display = 'block';
        if (loadingSpinner) loadingSpinner.style.display = 'none';
        if (errorMessage) errorMessage.style.display = 'none';
    }

    function displayCharacter(character) {
        console.log('📊 Displaying character data:', character);
        
        // Update character name
        if (characterNameEl) characterNameEl.textContent = character.name;
        if (characterNameDisplayEl) characterNameDisplayEl.textContent = character.name;
        
        // Update page title
        document.title = `${character.name} - Character Details`;
        
        // Display character information sections
        displayCharacterOverview(character);
        displayCharacterStats(character);
        displayCharacterSkills(character);
        displayCharacterTraits(character);
        displayCharacterEquipment(character);
    }

    function displayCharacterOverview(character) {
        const avatarSection = document.getElementById('character-avatar-section');
        const basicInfoSection = document.getElementById('character-basic-info');
        
        if (avatarSection) {
            if (character.avatarUrl) {
                avatarSection.innerHTML = `
                    <img src="${character.avatarUrl}" alt="${character.name}" class="character-avatar">
                `;
            } else {
                const initials = character.name.split(' ').map(name => name[0]).join('').toUpperCase();
                avatarSection.innerHTML = `
                    <div class="avatar-placeholder">${initials}</div>
                `;
            }
        }
        
        if (basicInfoSection) {
            basicInfoSection.innerHTML = `
                <div class="info-item">
                    <span class="info-label">Race</span>
                    <span class="info-value">${character.characterRace?.name || 'Unknown'}</span>
                </div>
                <div class="info-item">
                    <span class="info-label">Training Package</span>
                    <span class="info-value">${character.characterTrainingPackage?.name || 'None'}</span>
                </div>
                <div class="info-item">
                    <span class="info-label">SCL</span>
                    <span class="info-value">${character.scl || 0}</span>
                </div>
                <div class="info-item">
                    <span class="info-label">Unis</span>
                    <span class="info-value">${character.unis || 0}</span>
                </div>
                <div class="info-item">
                    <span class="info-label">Credits</span>
                    <span class="info-value">${character.credits || 0}</span>
                </div>
                <div class="info-item">
                    <span class="info-label">Finance Chip</span>
                    <span class="info-value">${character.hasFinanceChip ? 'Yes' : 'No'}</span>
                </div>
            `;
        }
    }

    function displayCharacterStats(character) {
        const statsContainer = document.getElementById('character-stats-container');
        if (!statsContainer || !character.stats) return;
        
        // Enhanced stat mapping with proper handling
        const getStatDisplayName = (statType) => {
            // Handle both string and numeric stat types
            if (typeof statType === 'string') {
                return statType.toUpperCase();
            }
            
            const statNames = {
                0: 'STR', 1: 'DEX', 2: 'KNOW', 
                3: 'CONC', 4: 'CHA', 5: 'COOL'
            };
            return statNames[statType] || statType.toString();
        };
        
        statsContainer.innerHTML = character.stats.map(stat => `
            <div class="stat-item">
                <span class="stat-name">${getStatDisplayName(stat.type)}</span>
                <span class="stat-value">${stat.value}</span>
                <div class="stat-bar">
                    <div class="stat-fill" style="width: ${(stat.value / 6) * 100}%"></div>
                </div>
            </div>
        `).join('');
    }

    function displayCharacterSkills(character) {
        const skillsContainer = document.getElementById('character-skills-container');
        if (!skillsContainer || !character.skills) return;
        
        // Enhanced stat mapping with proper handling for both view and print
        const getStatDisplayName = (statType) => {
            // Handle both string and numeric stat types
            if (typeof statType === 'string') {
                return statType.toUpperCase();
            }
            
            const statNames = {
                0: 'STR', 1: 'DEX', 2: 'KNOW', 
                3: 'CONC', 4: 'CHA', 5: 'COOL'
            };
            return statNames[statType] || 'MISC'; // Changed from 'UNKNOWN' to 'MISC'
        };
        
        // Group skills by stat type
        const groupedSkills = character.skills.reduce((groups, skill) => {
            const statName = getStatDisplayName(skill.relatedStat);
            if (!groups[statName]) groups[statName] = [];
            groups[statName].push(skill);
            return groups;
        }, {});
        
        skillsContainer.innerHTML = Object.entries(groupedSkills).map(([statName, skills]) => `
            <div class="skill-group">
                <h4 class="skill-group-title">${statName} Skills</h4>
                <div class="skills-list">
                    ${skills.map(skill => `
                        <div class="skill-item">
                            <span class="skill-name">${skill.name}</span>
                            <span class="skill-rank">Rank ${skill.rank}</span>
                        </div>
                    `).join('')}
                </div>
            </div>
        `).join('');
    }

    function displayCharacterTraits(character) {
        const traitsContainer = document.getElementById('character-traits-container');
        if (!traitsContainer) return;
        
        if (!character.characterTraits || character.characterTraits.length === 0) {
            traitsContainer.innerHTML = '<div class="no-data">No traits selected</div>';
            return;
        }
        
        traitsContainer.innerHTML = character.characterTraits.map(charTrait => `
            <div class="trait-item">
                <div class="trait-header">
                    <span class="trait-name">${charTrait.trait?.name || 'Unknown Trait'}</span>
                    <span class="trait-cost">${charTrait.trait?.basePointCost || 0} pts</span>
                </div>
                <p class="trait-description">${charTrait.trait?.description || 'No description available'}</p>
            </div>
        `).join('');
    }

    function displayCharacterEquipment(character) {
        const equipmentContainer = document.getElementById('equipment-container');
        if (!equipmentContainer) return;
        
        if (!character.characterEquipment || character.characterEquipment.length === 0) {
            equipmentContainer.innerHTML = '<div class="no-data">No equipment</div>';
            return;
        }
        
        const equipmentHtml = character.characterEquipment.map(item => {
            if (item.equipmentItem && item.quantity > 0) {
                const totalWeight = (item.equipmentItem.weight * item.quantity).toFixed(2);
                const totalCost = item.equipmentItem.cost * item.quantity;
                
                return `
                    <div class="equipment-item clickable-item" data-tooltip="Equipment: ${item.equipmentItem.name} x${item.quantity}">
                        <div class="equipment-header">
                            ${createEquipmentImage(item.equipmentItem)}
                            <div class="equipment-info">
                                <h4 class="equipment-name">${item.equipmentItem.name}</h4>
                                <span class="equipment-quantity">Qty: ${item.quantity}</span>
                            </div>
                        </div>
                        <p class="equipment-description">${item.equipmentItem.description || 'No description available'}</p>
                        <div class="equipment-stats">
                            <span class="equipment-stat">
                                <small class="equipment-stat-label">Cost Each</small>
                                <strong class="equipment-stat-value">${item.equipmentItem.cost}c</strong>
                            </span>
                            <span class="equipment-stat">
                                <small class="equipment-stat-label">Weight Each</small>
                                <strong class="equipment-stat-value">${item.equipmentItem.weight}</strong>
                            </span>
                            <span class="equipment-stat">
                                <small class="equipment-stat-label">Total Weight</small>
                                <strong class="equipment-stat-value">${totalWeight}</strong>
                            </span>
                            <span class="equipment-stat">
                                <small class="equipment-stat-label">Total Cost</small>
                                <strong class="equipment-stat-value">${totalCost}c</strong>
                            </span>
                        </div>
                    </div>
                `;
            }
            return '';
        }).filter(Boolean).join('');
        
        equipmentContainer.innerHTML = equipmentHtml;
    }

    function createEquipmentImage(equipmentItem) {
        // Don't create image if we know it will fail
        if (!equipmentItem.imageUrl || failedImages.has(equipmentItem.imageUrl)) {
            return `<div class="equipment-image equipment-placeholder">📦</div>`;
        }
        
        return `<img src="${equipmentItem.imageUrl}" alt="${equipmentItem.name}" class="equipment-image" 
            onerror="handleImageError(this, '${equipmentItem.imageUrl}')" loading="lazy">`;
    }

    // Global function for image error handling
    window.handleImageError = function(img, originalSrc) {
        failedImages.add(originalSrc);
        img.style.display = 'none';
        
        // Create placeholder
        const placeholder = document.createElement('div');
        placeholder.className = 'equipment-image equipment-placeholder';
        placeholder.textContent = '📦';
        placeholder.title = 'Image not available';
        
        img.parentNode.insertBefore(placeholder, img);
    };

    function addInteractiveFeatures() {
        // Add keyboard shortcuts
        document.addEventListener('keydown', function(e) {
            // ESC - Back to list
            if (e.key === 'Escape') {
                window.location.href = '/CharacterList';
            }
            // E - Edit character
            else if (e.key.toLowerCase() === 'e' && !e.ctrlKey && !e.altKey) {
                if (editCharacterBtn) editCharacterBtn.click();
            }
            // P - Print
            else if (e.key.toLowerCase() === 'p' && e.ctrlKey) {
                e.preventDefault();
                printCharacterSheet();
            }
            // D - Download PDF
            else if (e.key.toLowerCase() === 'd' && e.ctrlKey) {
                e.preventDefault();
                if (characterDataCache && downloadPdfBtn) {
                    downloadPdf(characterDataCache);
                }
            }
        });

        // Add tooltips
        addTooltips();
        
        // Add click highlighting
        addClickHighlighting();
    }

    function addTooltips() {
        const tooltipElements = document.querySelectorAll('[data-tooltip]');
        tooltipElements.forEach(element => {
            let tooltip = null;
            
            element.addEventListener('mouseenter', function(e) {
                const tooltipText = this.getAttribute('data-tooltip');
                if (!tooltipText) return;
                
                tooltip = document.createElement('div');
                tooltip.className = 'tooltip';
                tooltip.textContent = tooltipText;
                tooltip.style.position = 'absolute';
                tooltip.style.background = 'rgba(0, 0, 0, 0.9)';
                tooltip.style.color = 'white';
                tooltip.style.padding = '8px 12px';
                tooltip.style.borderRadius = '6px';
                tooltip.style.fontSize = '12px';
                tooltip.style.zIndex = '10000';
                tooltip.style.pointerEvents = 'none';
                tooltip.style.whiteSpace = 'nowrap';
                
                document.body.appendChild(tooltip);
                
                const rect = this.getBoundingClientRect();
                tooltip.style.left = (rect.left + rect.width / 2 - tooltip.offsetWidth / 2) + 'px';
                tooltip.style.top = (rect.top - tooltip.offsetHeight - 10) + 'px';
            });
            
            element.addEventListener('mouseleave', function() {
                if (tooltip) {
                    document.body.removeChild(tooltip);
                    tooltip = null;
                }
            });
        });
    }

    function addClickHighlighting() {
        const clickableItems = document.querySelectorAll('.clickable-item');
        clickableItems.forEach(item => {
            item.addEventListener('click', function() {
                // Remove existing highlights
                document.querySelectorAll('.highlighted').forEach(el => {
                    el.classList.remove('highlighted');
                });
                
                // Add highlight to clicked item
                this.classList.add('highlighted');
                
                // Remove highlight after 2 seconds
                setTimeout(() => {
                    this.classList.remove('highlighted');
                }, 2000);
            });
        });
    }

    // Error recovery functions
    function retryLoadCharacter() {
        characterDataCache = null;
        isLoading = false;
        loadCharacterData();
    }

    // Global error handler for unhandled promise rejections
    window.addEventListener('unhandledrejection', function(event) {
        console.error('❌ Unhandled promise rejection:', event.reason);
        
        // Prevent the default browser error handling
        event.preventDefault();
        
        // Show user-friendly error message
        if (characterDataCache === null) {
            showError();
        }
    });

    // Utility function to safely access nested object properties
    function safeGet(obj, path, defaultValue = null) {
        return path.split('.').reduce((current, key) => {
            return (current && current[key] !== undefined) ? current[key] : defaultValue;
        }, obj);
    }

    // Add global CSS for better PDF/Print styling
    const additionalStyles = document.createElement('style');
    additionalStyles.textContent = `
        @media print {
            .header-actions button,
            .collapse-btn,
            .quick-actions-card {
                display: none !important;
            }
            
            .character-card {
                break-inside: avoid;
                page-break-inside: avoid;
            }
            
            .character-sheet-modern {
                gap: 10px !important;
            }
        }
    `;
    document.head.appendChild(additionalStyles);

    console.log('✅ View character script loaded successfully');
});

// Global toggle function for section collapse
window.toggleSection = function(btn) {
    const cardBody = btn.closest('.character-card').querySelector('.card-body');
    const isCollapsed = cardBody.style.display === 'none';
    
    cardBody.style.display = isCollapsed ? 'block' : 'none';
    btn.textContent = isCollapsed ? '−' : '+';
    
    // Add animation
    if (isCollapsed) {
        cardBody.style.opacity = '0';
        cardBody.style.transform = 'translateY(-10px)';
        setTimeout(() => {
            cardBody.style.transition = 'opacity 0.3s ease, transform 0.3s ease';
            cardBody.style.opacity = '1';
            cardBody.style.transform = 'translateY(0)';
        }, 10);
    }
};