document.addEventListener('DOMContentLoaded', function () {
    const raceSelect = document.getElementById('races');
    const trainingPackageSelect = document.getElementById('training-packages');
    const characterForm = document.getElementById('character-form');

    const apiUrl = 'https://localhost:7183/api/CharacterApi';

    // Fetch races and populate the dropdown
    fetch(`${apiUrl}/races`)
        .then(response => response.json())
        .then(data => {
            data.forEach(race => {
                const option = document.createElement('option');
                option.value = race.id;
                option.textContent = race.name;
                raceSelect.appendChild(option);
            });
        });

    // Fetch training packages and populate the dropdown
    fetch(`${apiUrl}/training-packages`)
        .then(response => response.json())
        .then(data => {
            data.forEach(pkg => {
                const option = document.createElement('option');
                option.value = pkg.id;
                option.textContent = pkg.name;
                trainingPackageSelect.appendChild(option);
            });
        });

    characterForm.addEventListener('submit', function (e) {
        e.preventDefault();

        const characterData = {
            name: document.getElementById('name').value,
            raceId: raceSelect.value,
            trainingPackageId: trainingPackageSelect.value,
            userId: '00000000-0000-0000-0000-000000000001' // Replace with actual user ID
        };

        fetch(apiUrl, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(characterData),
        })
        .then(response => response.json())
        .then(data => {
            console.log('Character created:', data);
            alert('Character created successfully!');
        })
        .catch((error) => {
            console.error('Error:', error);
            alert('Error creating character.');
        });
    });
});
