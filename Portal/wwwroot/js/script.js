document.addEventListener('DOMContentLoaded', () => {
    console.log('script.js loaded.');

    const gameDropdown = document.getElementById('SelectedBoardGameId');
    const is18PlusCheckbox = document.getElementById('Is18Plus');

    // Function to update 18+ checkbox based on selected game
    function update18PlusCheckbox() {
        const selectedGame = gameDropdown.options[gameDropdown.selectedIndex];
        const is18Plus = selectedGame.getAttribute('data-is18plus') === 'True';

        console.log('Is 18+:', is18Plus);

        is18PlusCheckbox.checked = is18Plus;
        is18PlusCheckbox.disabled = is18Plus;

        console.log('Checkbox updated - Checked:', is18PlusCheckbox.checked, 'Disabled:', is18PlusCheckbox.disabled);
    }

    // Add event listener directly to the game dropdown
    gameDropdown.addEventListener('change', update18PlusCheckbox);

    // Initialize 18+ checkbox based on initial game selection
    update18PlusCheckbox();
});
