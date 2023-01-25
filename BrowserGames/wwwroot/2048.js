

updateTile = function (tile, number) {
    console.log("updateTile( " + tile + ", " + number + " )");
    tile.innerText = "";
    tile.classList.value = "";
    tile.classList.add("tile");
    if (number > 0) {
        tile.innerText = number;
        if (num <= 4096) {
            tile.classList.add("x" + number.toString());
        }
        else {
            tile.classList.add("x8192");
        }
    }
};
