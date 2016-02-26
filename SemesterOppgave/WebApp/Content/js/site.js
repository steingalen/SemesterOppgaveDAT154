document.addEventListener("DOMContentLoaded", function () {
    /* ================================================================================================================
                                                     Global variables
       ================================================================================================================ */
    // Modals
    var singInModal = document.getElementById("sign-in-modal");
    var singUpModal = document.getElementById("sign-up-modal");
    var reservationModal = document.getElementById("reservation-modal");
    // Buttons
    var signInModalButton = document.getElementById("sign-in-modal-button");
    var signUpnModalButton = document.getElementById("sign-up-modal-button");
    var signOutButton = document.getElementById("logout-button");
    // Dropdowns
    var qualityDropDown = document.getElementById("roomQuality");
    var sizeDropDown = document.getElementById("roomSize");
    var bedsDropDown = document.getElementById("roomBeds");
    // Textfields
    var startTextField = document.getElementById("startTextField");
    var endTextField = document.getElementById("endTextField");
    // Table
    var table = document.getElementById("resultTable");
    var selectedRow = null;
    var highlightRowColor = "#4f94cf";
    var defaultRowColor = "white";
    // Cached
    var startDate;
    var endDate;
    var email;
    var ticket;
    // HttpRequests
    var baseUrl = "http://localhost:3274/Ajax/";
    var xhr;
    if (window.XMLHttpRequest) {
        xhr = new XMLHttpRequest();
    } else {
        xhr = new ActiveXObject("Microsoft.XMLHTTP");
    }


    /* ================================================================================================================
                                                 Event listeners
       ================================================================================================================ */
    document.getElementById("search-button").addEventListener("click", search);
    document.getElementById("reservation-button").addEventListener("click", makeReservation);
    signInModalButton.addEventListener("click", showSignIn);
    signUpnModalButton.addEventListener("click", showSignUp);
    //signOutButton.addEventListener("click", signOut);
    document.getElementById("email-button").addEventListener("click", reset);
    document.getElementById("sign-in-to-sign-up").addEventListener("click", showSignUp);
    document.getElementById("sign-up-to-sign-in").addEventListener("click", showSignIn);
    document.getElementById("sign-in-button").addEventListener("click", signIn);
    document.getElementById("sign-up-button").addEventListener("click", signUp);
    document.addEventListener("keydown", escClicked);

    /* ================================================================================================================
                                                     Main functions 
       ================================================================================================================ */

    // Resets search filters and deletes all element from table
    function reset() {
        closeModals();
        table.removeChild(table.getElementsByTagName("tbody")[0]);
        startTextField.value = "";
        endTextField.value = "";
        qualityDropDown.selectedIndex = 0;
        sizeDropDown.selectedIndex = 0;
        bedsDropDown.selectedIndex = 0;
    }

    // Hides sign ins, and shows logout.
    function trySignIn(data) {       
        var json = JSON.parse(JSON.parse(data.responseText));
        if (json.Success) {
            signUpnModalButton.style.display = "none";
            signOutButton.style.display = "inline-block";
            signOutButton.value = signOutButton.value + " " + json.FirstName + " "+ json.LastName;
            signInModalButton.style.display = "none";
            ticket = json.Ticket;
            closeModals();
        } else {
            showSignIn();
        }
    }

    // Sign up click event
    function signUp() {
        email = document.getElementById("signUpEmail").value;
        var password = document.getElementById("signUpPassword").value;
        var lastname = document.getElementById("signUpLastname").value;
        var firstname = document.getElementById("signUpFirstname").value;
        ajax("POST", baseUrl + "signup", JSON.stringify({ "Email": email, "Password": password, "FirstName": firstname, "LastName": lastname }), trySignIn, ajaxError);
    }

    // Sign in click event
    function signIn() {
        email = document.getElementById("email").value;
        var password = document.getElementById("password").value;
        ajax("POST", baseUrl + "signin", JSON.stringify({ "Email": email, "Password": password }), trySignIn, ajaxError);
    }

    // search click event
    function search() {
        startDate = startTextField.value;
        endDate = endTextField.value;
        var searchParamater = "/?Quality=" + qualityDropDown.options[qualityDropDown.selectedIndex].value +
                                "&Size=" + sizeDropDown.options[sizeDropDown.selectedIndex].value +
                                "&Beds=" + bedsDropDown.options[bedsDropDown.selectedIndex].value +
                                "&Start=" + startDate +
                                "&End=" + endDate;
        table.removeChild(table.getElementsByTagName("tbody")[0]);
        ajax("GET", baseUrl + "Search" + searchParamater, null, populateTable, ajaxError);
    }

    // make reservation click event
    function makeReservation() {

        if (selectedRow == null) {
            alert("Select a row first!");
            return;
        }
   
        var reservationJson = {
                "Start": startDate,
                "End": endDate,
                "Quality": selectedRow[0].innerHTML,
                "Size": selectedRow[1].innerHTML,
                "Beds": selectedRow[2].innerHTML,
                "Email": email
        }
        
        ajax("POST", baseUrl + "makeReservation", JSON.stringify(reservationJson),
            function (data) {
                var reservation = JSON.parse(JSON.parse(data.responseText));
                
                document.getElementById("reservationFirstname").value = reservation.Customer.FirstName;
                document.getElementById("reservationLastname").value = reservation.Customer.LastName;
                document.getElementById("reservationEmail").value = reservation.Customer.Email;
                document.getElementById("reservationStart").value = reservation.Start.split("T")[0];
                document.getElementById("reservationEnd").value = reservation.Slutt.split("T")[0];
                document.getElementById("reservationSize").value = reservation.Room.Size.Size;
                document.getElementById("reservationQuality").value = reservation.Room.Quality.Quality;
                document.getElementById("reservationBeds").value = reservation.Room.Beds.Beds;
                //document.getElementById("reservationPrice").value = reservation.Price;

                showReservation();

        },ajaxError);
    }


    /* ================================================================================================================
                                                         UI functions 
       ================================================================================================================ */
    // Shows Sign in modal
    function showSignIn() {
        closeModals();
        singInModal.style.display = "inline-block";
    }

    // Shows sign up modal
    function showSignUp() {
        closeModals();
        singUpModal.style.display = "inline-block";
    }

    // Shows sign up modal
    function showReservation() {
        closeModals();
        reservationModal.style.display = "inline-block";
    }

    // When the user clicks anywhere outside of the modal, close it
    function escClicked(e) {
        if (e.keyCode === 27) {
            closeModals();
        }
    }

    // Closes modals
    function closeModals() {
        singUpModal.style.display = "none";
        singInModal.style.display = "none";
        reservationModal.style.display = "none";
    }

    /* ================================================================================================================
                                                     Helper functions 
       ================================================================================================================ */

    // Add click functions to table rows
    function setRowClickHandlers() {
        var rows = table.getElementsByTagName("tr");
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
                    colorRow(selectedRow, defaultRowColor);

                if (selectedRow === cells)
                    colorRow(selectedRow, defaultRowColor);
                else
                    colorRow(cells, highlightRowColor);

                selectedRow = cells;

            } catch (err) { }
        };
    };

    // Colors a table row
    function colorRow(list, color) {
        for (var i = 0; i < list.length; i++)
            list[i].style.background = color;
    }

    // General ajax call
    function ajax(method, url, data, callbackOk, callbackError) {
        xhr = new XMLHttpRequest();
        xhr.open(method, url);
        xhr.setRequestHeader("Content-Type", "application/json");
        xhr.send(data);
        
        xhr.onreadystatechange = function () {
            if (xhr.readyState === 4 && xhr.status === 200) 
                callbackOk(xhr);
            else 
                callbackError();
        }
    };

    // Adds a cell to a row
    function addRowToTable(row, cell, value) {
        var newCell = row.insertCell(cell);
        var newText = document.createTextNode(value);
        newCell.appendChild(newText);
    }

    // Populates the table with data
    function populateTable(data) {
        var newTbody = document.createElement('tbody');      
        var jsonArray = JSON.parse(JSON.parse(data.responseText));

        if(jsonArray == undefined)
            return;

        // Adds a new row to the table and populate it
        for (var i = 0, len = jsonArray.length; i < len; i++) {

            var newRow = newTbody.insertRow(newTbody.rows.length);
            addRowToTable(newRow, 0, jsonArray[i].Quality.Quality);
            addRowToTable(newRow, 1, jsonArray[i].Size.Size);
            addRowToTable(newRow, 2, jsonArray[i].Beds.Beds);
        }
        table.appendChild(newTbody);

        setRowClickHandlers();
   }

    // Handles ajax errors
    function ajaxError() {
    }

}, false);
