$(document).ready(function () {
    assignCellInteractions();
});

// Assign interactions to cells
function assignCellInteractions() {
    $('.cell').on('mousedown', function (event) {
        var cell = $(this);

        if (event.which === 1) {
            // Left click
            revealCell(cell);
        } else if (event.which === 3) {
            // Right click
            markCell(cell);
        }

        return false; // Prevents the context menu from appearing
    });
}

// Reveal a cell
function revealCell(cell) {
    cell.addClass('revealed');
}

// Mark a cell
function markCell(cell) {
    cell.addClass('flagged');
}
function saveGame() {
  
    var gameState = JSON.stringify(getGameState()); 

    $.ajax({
        type: "POST",
        url: "/Minesweeper/SaveGame",
        data: {
            userId: $("#userId").val(),
            timestamp: new Date().toISOString(),
            gameState: gameState
        },
        success: function () {
            alert("Game saved successfully!");
        },
        error: function () {
            alert("An error occurred while saving the game.");
        }
    });
}