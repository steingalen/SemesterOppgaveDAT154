$(document).ready(function () {

    /*
    =======================================================================================================================
                                                        Global variables
    =======================================================================================================================
    */
    var table = $("#roomTable");
    var highlightRowBackgroundColor = "#333333";
    var defaultRowBackgroundColor = "white";
    var highlightRowColor = "white";
    var defaultRowColor = "#333333";
    var selectedRow;
    var baseUrl = "http://localhost:2517/Ajax/";


    /*
    =======================================================================================================================
                                                        Main functions
    =======================================================================================================================
    */

    // Search functions
    function search() {

        var searchParamater = "/?Quality=" + $("#roomQuality option:selected").val() +
            "&Size=" + $("#roomSize option:selected").val() +
            "&Beds=" + $("#roomBeds option:selected").val() +
            "&Start=" + $("#start").val() +
            "&End=" + $("#end").val();

        try {

            table.find("tbody")[0].remove();
        } catch (e) {
        }
        $.ajax({
            type: "GET",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'text/plain'
            },
            dataType: "json",
            url: baseUrl + "Search" + searchParamater,
            success: populateTable
        });
    }

    // Makes a reservation
    function makeReservation() {
        if (typeof selectedRow == "undefined")
            return;

        var reservationJson = {
            "Start": $("#start").val(),
            "End": $("#end").val(),
            "Quality": selectedRow[0].innerHTML,
            "Size": selectedRow[1].innerHTML,
            "Beds": selectedRow[2].innerHTML
        }

        console.log("Quality: " + selectedRow[0].innerHTML + ", Size: " + selectedRow[1].innerHTML + ", Beds: " + selectedRow[2].innerHTML);

        $.ajax({
            type: "POST",
            data: reservationJson,
            dataType: "json",
            url: baseUrl + "makeReservation",
            error: function (data) {
                alert("Have you logged in?");
            },
            success: function (data) {
                var reservation = JSON.parse(data);

                if (reservation.Message === "An error has occurred.") {
                    alert("An error has occured.");
                    return;
                }

                $("#modal").modal("toggle");
                $("#reservationFirstname").val(reservation.Customer.FirstName);
                $("#reservationLastname").val(reservation.Customer.LastName);
                $("#reservationEmail").val(reservation.Customer.Email);
                $("#reservationStart").val(reservation.Start.split("T")[0]);
                $("#reservationEnd").val(reservation.Slutt.split("T")[0]);
                $("#reservationSize").val(reservation.Room.Size.Size);
                $("#reservationQuality").val(reservation.Room.Quality.Quality);
                $("#reservationBeds").val(reservation.Room.Beds.Beds);
                $("#price").val(reservation.Price);


                colorRow(selectedRow, defaultRowBackgroundColor, defaultRowColor);
                selectedRow = null;
                search();
            }
        });
    }

    /*
    =======================================================================================================================
                                                        Click events
    =======================================================================================================================
    */

    // Search button
    $("#search").click(function() {search();});

    // reservation button
    $("#makeReservation").click(function () { makeReservation();});

    /*
    =======================================================================================================================
                                                        General functions
    =======================================================================================================================
    */
    // Populates the table with data
    function populateTable(data) {
        var newTbody = document.createElement('tbody');
        var jsonArray = JSON.parse(data);

        if (jsonArray == undefined)
            return;

        // Adds a new row to the table and populate it
        for (var i = 0, len = jsonArray.length; i < len; i++) {

            var newRow = newTbody.insertRow(newTbody.rows.length);
            addRowToTable(newRow, 0, jsonArray[i].Quality.Quality);
            addRowToTable(newRow, 1, jsonArray[i].Size.Size);
            addRowToTable(newRow, 2, jsonArray[i].Beds.Beds);
        }
        table.append(newTbody);

        setRowClickHandlers();
    }

    // Adds a cell to a row
    function addRowToTable(row, cell, value) {
        var newCell = row.insertCell(cell);
        var newText = document.createTextNode(value);
        newCell.appendChild(newText);
    }

    
    // Add click functions to table rows
    function setRowClickHandlers() {
        var rows = table.find("tr");
        for (var i = 0; i < rows.length; i++) {
            var currentRow = rows[i];

            currentRow.onclick = createClickHandlerForRow(currentRow);
        }
    }
    // Click handler for a row
    function createClickHandlerForRow(row) {
        return function () {
            try {
                var cells = row.getElementsByTagName("td");
                if (cells == null)
                    return;

                if (selectedRow != null)
                    colorRow(selectedRow, defaultRowBackgroundColor, defaultRowColor);

                if (selectedRow === cells)
                    colorRow(selectedRow, defaultRowBackgroundColor, defaultRowColor);
                else
                    colorRow(cells, highlightRowBackgroundColor, highlightRowColor);

                selectedRow = cells;

            } catch (err) { }
        };
    };

    // Colors a table row
    function colorRow(list, background, color) {
        for (var i = 0; i < list.length; i++) {
            list[i].style.background = background;
            list[i].style.color = color;
        }
    }   
})