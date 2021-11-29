"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/voting-hub").build();

//Disable the send button until connection is established.
document.getElementById("voteButton").disabled = true;

connection.on("VoteNotification", function(votingSessionId) {
    var sessionId = $("#VotingSessionId").val();
    if (sessionId !== votingSessionId) {
        return;
        //TODO use hub groups to notify only clients for current session and not all
    }

    $.ajax({
        type: 'GET',
        url: '/Vote/GetVoteResult?votingId=' + votingSessionId,
        success: function(data) {
            $('#votes').html(data);
        }
    });
});

connection.start().then(function() {
    document.getElementById("voteButton").disabled = false;
}).catch(function(err) {
    return console.error(err.toString());
});

document.getElementById("voteButton").addEventListener("click", function(event) {
    event.preventDefault();
    var restaurant = $("option:selected", $("#restaurantId")).attr("value");
    var user = $('#user').val();
    var sessionId = $('#VotingSessionId').val();
    var validationError = "";
    if (!restaurant) {
        validationError = "Nevybrána restaurace. <br/>";
    }
    if (!user) {
        validationError += "Zadejte jméno.";
    }

    var validationDiv = $(".validation");

    if (validationError !== "") {
        validationDiv.html(validationError);

        return;
    }

    validationDiv.text("");

    $.ajax({
        type: 'POST',
        url: '/Vote/Vote',
        data: {
            restaurantId: restaurant,
            user: user,
            votingSessionId: sessionId
        },
        success: function() {
            connection.invoke("NotifySessionAsync", sessionId).catch(function(err) {
                return console.error(err.toString());
            });
        }
    });
});