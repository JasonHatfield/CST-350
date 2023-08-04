window.onload = function () {

    var userId = document.getElementById('userId').value;

    console.log('User ID in JavaScript: ' + userId);


    // Game variables
    var cells = document.querySelectorAll('.cell');
    var totalCells = cells.length;
    var totalMines = 10;
    var gameStarted = false;
    var gameEnded = false;
    var lastClickedCell = null;

    // Set up event listeners and initial cell states
    cells.forEach(cell => {
        cell.dataset.state = 'hidden';
        cell.addEventListener('contextmenu', rightClickEvent);
        cell.addEventListener('click', leftClickEvent);
    });

    // Handle right-click event on cells
    function rightClickEvent(e) {
        e.preventDefault();
        if (gameEnded || this.dataset.state === 'revealed') return;
        handleRightClick(this);
        checkWinCondition(cells);
    }

    // Handle left-click event on cells
    function leftClickEvent(e) {
        e.preventDefault();
        if (gameEnded) {
            resetGame();
            return;
        }
        if (!gameStarted) {
            startGame(cells, totalMines, this);
            gameStarted = true;
        }
        lastClickedCell = this;

        if (this.dataset.state === 'flagged' || this.dataset.state === 'question') {
            return;
        }

        if (this.dataset.hasMine) {
            this.style.backgroundColor = '#ff0000';
            this.style.border = '1px solid #000';
            this.innerHTML = '💣';
            gameEnded = true;
            setTimeout(showGameOverPopUp, 100);
        } else {
            revealCellContents(this, cells);
            if (!gameEnded) checkWinCondition(cells);
        }
    }

    // Handle right-click event on cells to toggle state
    function handleRightClick(cell) {
        var states = ['hidden', 'flagged', 'question'];
        var icons = ['', '<span class="icon">🚩</span>', '<span class="icon">?</span>'];
        var index = states.indexOf(cell.dataset.state);
        cell.dataset.state = states[(index + 1) % states.length];
        cell.innerHTML = icons[(index + 1) % states.length];
    }

    // Get random indices for mine placement
    function getRandomIndices(totalCells, totalMines, clickedCellIndex) {
        var indices = Array.from({ length: totalCells }, (_, index) => index)
            .filter(index => index !== clickedCellIndex)
            .sort(() => Math.random() - 0.5);
        return indices.slice(0, totalMines);
    }

    // Recursive function to reveal cell and its adjacent cells
    function revealCellContents(cell, cells) {
        if (cell.dataset.state === 'revealed') return;
        if (cell.dataset.hasMine) {
            cell.style.backgroundColor = '#ff0000';
            cell.innerHTML = '💣';  // Mine icon
            gameEnded = true;
            setTimeout(showGameOverPopUp, 100);
            return;
        }

        var adjacentCells = getAdjacentCells(cell, cells);
        var mineCount = adjacentCells.filter(adjCell => adjCell.dataset.hasMine).length;
        revealCell(cell, mineCount);
        if (mineCount === 0) adjacentCells.forEach(adjCell => revealCellContents(adjCell, cells));
    }

    // Get adjacent cells of a given cell
    function getAdjacentCells(cell, cells) {
        var cellIndex = Array.prototype.indexOf.call(cells, cell);
        var rowIndex = Math.floor(cellIndex / 10);
        var colIndex = cellIndex % 10;
        var adjacentCells = [];
        for (var i = Math.max(0, rowIndex - 1); i <= Math.min(9, rowIndex + 1); i++)
            for (var j = Math.max(0, colIndex - 1); j <= Math.min(9, colIndex + 1); j++)
                if (i !== rowIndex || j !== colIndex) adjacentCells.push(cells[i * 10 + j]);
        return adjacentCells;
    }

    // Reveal cell with mine count and update style
    function revealCell(cell, mineCount) {
        cell.dataset.state = 'revealed';
        cell.innerHTML = mineCount > 0 ? mineCount : '';
        cell.style.backgroundColor = '#a0a0a0';
    }

    // Generate mines on the board
    function generateMines(cells, totalMines, clickedCell) {
        getRandomIndices(totalCells, totalMines, Array.prototype.indexOf.call(cells, clickedCell))
            .forEach(index => cells[index].dataset.hasMine = true);
    }

    // Start the game by generating mines
    function startGame(cells, totalMines, clickedCell) {
        generateMines(cells, totalMines, clickedCell);
    }

    // Check win condition when all non-mine cells are revealed
    function checkWinCondition(cells) {
        var allNonMineCellsRevealed = Array.from(cells).every(cell => cell.dataset.hasMine || cell.dataset.state === 'revealed');
        if (allNonMineCellsRevealed) {
            revealAllCells(cells);
            gameEnded = true;
            setTimeout(showWinPopUp, 100);
        }
    }

    // Function to reveal all cells
    function revealAllCells(cells) {
        cells.forEach(cell => {
            if (cell.dataset.state !== 'revealed') {
                var adjacentCells = getAdjacentCells(cell, cells);
                var mineCount = adjacentCells.filter(adjCell => adjCell.dataset.hasMine).length;
                revealCell(cell, mineCount);
            }
        });
    }

    // Show game over pop-up and reveal remaining cells
    function showGameOverPopUp() {
        var cells = document.querySelectorAll('.cell');
        cells.forEach(cell => {
            if (cell.dataset.state !== 'revealed') {
                if (cell.dataset.hasMine) {
                    cell.style.backgroundColor = '#ff0000';
                    cell.innerHTML = '💣';
                } else {
                    var adjacentCells = getAdjacentCells(cell, cells);
                    var mineCount = adjacentCells.filter(adjCell => adjCell.dataset.hasMine).length;
                    revealCell(cell, mineCount);
                }
            }
        });
        setTimeout(function () {
            alert('Game Over!');
        }, 100);
    }

    // Show win pop-up and reveal remaining cells
    function showWinPopUp() {
        var cells = document.querySelectorAll('.cell');
        cells.forEach(cell => {
            if (cell.dataset.state !== 'revealed') {
                var adjacentCells = getAdjacentCells(cell, cells);
                var mineCount = adjacentCells.filter(adjCell => adjCell.dataset.hasMine).length;
                revealCell(cell, mineCount);
            }
        });

        // Reveal cells with bombs
        cells.forEach(cell => {
            if (cell.dataset.hasMine) {
                cell.style.backgroundColor = '#ff0000';
                cell.innerHTML = '💣';
            }
        });

        setTimeout(function () {
            alert('Congratulations! You revealed all cells without hitting a mine!');
        }, 100);
    }

    // Reset the game state
    function resetGame() {
        cells.forEach(cell => {
            cell.dataset.state = 'hidden';
            cell.style.backgroundColor = '';
            cell.style.border = '';
            cell.innerHTML = '';
            cell.dataset.hasMine = '';
        });
        gameStarted = false;
        gameEnded = false;
        lastClickedCell = null;
    }

    function captureGameState(cells) {
        var gameState = Array.from(cells).map(cell => ({
            state: cell.dataset.state,
            hasMine: !!cell.dataset.hasMine
        }));
        return gameState;
    }


    $('#saveGameButton').click(function () {
        var gameState = captureGameState(cells);
        console.log('userId in AJAX call: ' + userId);
        var timestamp = new Date().toISOString();

        $.ajax({
            url: '/Minesweeper/SaveGame',
            type: 'POST',
            data: {
                userId: userId,
                timestamp: timestamp,
                gameState: JSON.stringify(gameState)
            },
            success: function (response) {
                alert('Game saved successfully!');
            },
            error: function (error) {
                alert('Failed to save game.');
            }
        });
    });

    $(document).on("click", ".deleteGameButton", function () {
        var gameId = $(this).data("gameid");

        $.ajax({
            url: '/Minesweeper/DeleteGame',
            type: 'POST',
            data: {
                gameId: gameId
            },
            success: function (response) {
                // If the game is deleted successfully, you may want to remove the corresponding
                // DOM element from the page, or update the list of saved games accordingly.
                alert(response.message);
                // Reload the page or update the saved games list if needed.
                location.reload();
            },
            error: function (error) {
                alert('Failed to delete game.');
            }
        });
    });
};
