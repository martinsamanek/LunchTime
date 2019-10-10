'use strict';
$(document).ready(function () {
    $('#registerModal').on('show.bs.modal', function (e) {
        $('#loginModal').modal('hide');
        $(this).find('.alert').hide();
    });

    $('#loginModal').on('show.bs.modal', function (e) {
        $(this).find('.alert').hide();
    });
    
    $('#loginForm, #registerForm').on('submit', function (e) {
        e.preventDefault();
        var $form = $(this);
        var errorContainer = $form.find('.alert').hide();
        
        ajaxSendForm($form, function() {
            window.location.reload(true);
        }, function(error) {
            errorContainer.html(error.responseJSON.value.message).show();
        });
    });
    
    $('.logout').on('click', function (e) {
        e.preventDefault();
        $('#logoutForm').submit();
    });
    
    $('.js-vote').on('click', function (e) {
        e.preventDefault();
        var $self = $(this);
        var restaurantId = $self.data('id');
        var $votesCountEl = $self.parent().find('.votes-count');
        var numberOfVotes = parseInt($votesCountEl.html());
        
        $.ajax({
            type: 'POST',
            url: window.appConfig.voteEndpoint,
            data: "restaurantId=" + restaurantId,
            success: function() {
                $self.toggleClass('voted');
                if ($self.hasClass('voted')) {
                    $votesCountEl.html(++numberOfVotes);
                } else {
                    $votesCountEl.html(--numberOfVotes);
                }
            },
            error: function(error) {
                alert(error.responseJSON.value.message);
            }
        })
    });
    
    function ajaxSendForm($form, successCallback, errorCallback) {
        $.ajax({
            type: $form.attr('method'),
            url: $form.attr('action'),
            data: $form.serialize(),
            success: successCallback,
            error: errorCallback
        });    
    }
});