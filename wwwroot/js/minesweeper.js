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
function getGameState() {
    var gameState = [];
    var cells = $('.cell');

    // Loop through each cell and extract its state
    cells.each(function () {
        var cell = $(this);
        var isRevealed = cell.hasClass('revealed');
        var isFlagged = cell.hasClass('flagged');
        var hasMine = cell.data('has-mine');
        var neighboringMines = parseInt(cell.data('neighboring-mines'));

        var cellState = {
            revealed: isRevealed,
            flagged: isFlagged,
            hasMine: hasMine,
            neighboringMines: neighboringMines
        };

        gameState.push(cellState);
    });

    return gameState;
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