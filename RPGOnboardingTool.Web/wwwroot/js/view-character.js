document.addEventListener('DOMContentLoaded', function () {
    const characterId = window.location.pathname.split('/').pop();

    if (!characterId) {
        document.body.innerHTML = '<h1>No character ID provided.</h1>';
        return;
    }

    fetch(`/api/CharacterApi/${characterId}`)
        .then(response => {
            if (!response.ok) {
                throw new Error('Character not found.');
            }
            return response.json();
        })
        .then(character => {
            document.getElementById('characterName').textContent = character.name;
            document.getElementById('characterRace').textContent = character.characterRace.name;
            document.getElementById('characterPackage').textContent = character.characterTrainingPackage.name;

            const statsGrid = document.getElementById('stats-grid');
            statsGrid.innerHTML = '';
            character.stats.forEach(stat => {
                statsGrid.innerHTML += `<div><strong>${stat.type}:</strong> ${stat.value}</div>`;
            });

            const skillsList = document.getElementById('skills-list');
            skillsList.innerHTML = '';
            if (character.skills.length > 0) {
                character.skills.forEach(skill => {
                    skillsList.innerHTML += `<li>${skill.name} - Rank ${skill.rank}</li>`;
                });
            } else {
                skillsList.innerHTML = '<li>No skills selected.</li>';
            }


            const traitsList = document.getElementById('traits-list');
            traitsList.innerHTML = '';
            if (character.characterTraits.length > 0) {
                character.characterTraits.forEach(ct => {
                    traitsList.innerHTML += `<li>${ct.trait.name}</li>`;
                });
            } else {
                traitsList.innerHTML = '<li>No traits selected.</li>';
            }


            const equipmentList = document.getElementById('equipment-list');
            equipmentList.innerHTML = '';
            if (character.characterEquipment.length > 0) {
                character.characterEquipment.forEach(ce => {
                    equipmentList.innerHTML += `<li>${ce.equipmentItem.name} (x${ce.quantity})</li>`;
                });
            } else {
                equipmentList.innerHTML = '<li>No equipment purchased.</li>';
            }


            document.getElementById('hp').textContent = `${character.hitPoints} / ${character.maxHitPoints}`;
            document.getElementById('credits').textContent = character.credits;
            document.getElementById('movement').textContent = character.movement;
            document.getElementById('encumbrance').textContent = `${character.currentWeightCarried} / ${character.encumbranceValue}`;

            const editButton = document.getElementById('editCharacter');
            editButton.href = `/EditCharacter/${characterId}`;
        })
        .catch(error => {
            console.error('Error fetching character details:', error);
            document.querySelector('.character-sheet').innerHTML = `<div class="error-message"><h1>Error: ${error.message}</h1><p>Could not load character data. Please try again later.</p></div>`;
        });
});