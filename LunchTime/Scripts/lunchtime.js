'use strict';
var DATA_ID = 'id',
    COOKIE_EXPIRATION_DAYS = 365,
    COOKIE_NAME,
    BOOKMARK_LINK_SELECTOR = '.js-bookmark',
    BOOKMARKED_CLASS = 'bookmarked';

function init(cookieName) {
    COOKIE_NAME = cookieName;
    registerHandlers();
}

function registerHandlers() {
    $(BOOKMARK_LINK_SELECTOR).on('click',function() {
        var $link = $(this);
        var id = $link.data(DATA_ID);
        var bookmarked = addOrRemoveBookmarked(id);

        $link.toggleClass(BOOKMARKED_CLASS, bookmarked.includes(id));
        setBookmarkCookie(bookmarked);        
    });
}

function addOrRemoveBookmarked(id) {
    var bookmarked = getBookmarkedArray();
    var index = bookmarked.indexOf(id);
    if (index > -1) {
        bookmarked.splice(index, 1);
    } else {
        bookmarked.push(id);
    }

    return bookmarked;
}

function setBookmarkCookie(bookmarkedArray) {
    if (bookmarkedArray.length === 0) {
        $.removeCookie(COOKIE_NAME);
        return;
    }    

    $.cookie(COOKIE_NAME, JSON.stringify(bookmarkedArray), { expires: COOKIE_EXPIRATION_DAYS });
}

function getBookmarkedArray() {
    var cookieValue = $.cookie(COOKIE_NAME);
    if (cookieValue === undefined) {
        return [];
    }

    return JSON.parse($.cookie(COOKIE_NAME));
}