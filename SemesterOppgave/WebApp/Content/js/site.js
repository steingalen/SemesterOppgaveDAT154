document.addEventListener("DOMContentLoaded", function () {
    // Event listeners
    document.getElementById("sign-in-button").addEventListener("click", ShowSignIn);
    document.getElementById("sign-up-button").addEventListener("click", ShowSignUp);
    document.addEventListener("keydown", EscClicked);

    // Global variables
    var selectedRow = null;
    var reservationJson = null;
    var highlightRowColor = "#4f94cf";
    var defaultRowColor = "white";
    var errorColor = "red";
    var defaultColor = "black";
    var singInModal = document.getElementById("sign-in-modal");
    var singUpModal = document.getElementById("sign-up-modal");
    var reservationModal = document.getElementById("reservation-modal");

    // Shows Sign in modal
    function ShowSignIn() {
        CloseModals();
        singInModal.style.display = "inline-block";
    }

    // Shows sign up modal
    function ShowSignUp() {
        CloseModals();
        singUpModal.style.display = "inline-block";
    }

    // Shows sign up modal
    function ShowReservation() {
        CloseModals();
        reservationModal.style.display = "inline-block";
    }

    // When the user clicks anywhere outside of the modal, close it
    function EscClicked (e) {
        if (e.keyCode == 27)
            CloseModals();
    }

    // Closes modals
    function CloseModals() {
        singUpModal.style.display = "none";
        singInModal.style.display = "none";
        reservationModal.style.display = "none";
    }

    // Add click functions to table rows
    function addRowHandlers() {
        var rows = document.getElementById("resultTable").getElementsByTagName("tr");
        for (i = 0; i < rows.length; i++) {
            var currentRow = rows[i];
            
            currentRow.onclick = createClickHandler(currentRow);
        }
    }

    // Click handler for a row
    var createClickHandler = function (row) {
        return function () {
            try {
                var cells = row.getElementsByTagName("td");
                if (cells == null)
                    return;


                if (selectedRow != null)
                    colorRow(selectedRow, defaultRowColor);

                if (selectedRow == cells)
                    colorRow(selectedRow, defaultRowColor);
                else
                    colorRow(cells, highlightRowColor);

                selectedRow = cells;

            } catch (err) { }
        };
    };

    function reserve() {

        // bruk selected row for å hente data

        // error handling
        document.getElementById("start").value == "needs a value";
        document.getElementById("start").style.color = errorColor;
        document.getElementById("end").value == "needs a value";
        document.getElementById("end").style.color = errorColor;
        // Hvis ikke fylt ut tid
        if (document.getElementById("start").value == "" ) {
            document.getElementById("start").style.color = errorColor;
            return;
        } else if(document.getElementById("end").value == "") {
            document.getElementById("end").style.color = errorColor;
        }

        // Ingen error lengre
        if (document.getElementById("start").style.color == errorColor ) {
            document.getElementById("start").style.color = defaultColor;
        } else if(document.getElementById("end").style.color == errorColor ) {
            document.getElementById("end").style.color = defaultColor;
        }


        reservationJson = {
            "quality": cells[0].innerHTML,
            "size": cells[1].innerHTML,
            "beds": cells[2].innerHTML,
            "start": document.getElementById("start"),
            "end": document.getElementById("end")
        }
    }

    function colorRow(list, color) {
        for (var i = 0; i < list.length; i++)
            list[i].style.background = color;
    }

    addRowHandlers();
}, false);
