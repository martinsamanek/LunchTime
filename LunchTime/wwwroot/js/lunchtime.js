'use strict';
var DATA_ID = 'id',
    COOKIE_EXPIRATION_DAYS = 365,
    BOOKMARK_COOKIE_NAME,
    BOOKMARK_LINK_SELECTOR = '.js-bookmark',
    BOOKMARKED_CLASS = 'bookmarked',
    CITY_COOKIE_NAME,
    CITY_SELECTOR = '.city';

function init(bookmarkCookieName, cityCookieName) {
    BOOKMARK_COOKIE_NAME = bookmarkCookieName;
    CITY_COOKIE_NAME = cityCookieName;
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
    $(CITY_SELECTOR).on('change', cityChanged);
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
        $.removeCookie(BOOKMARK_COOKIE_NAME);
        return;
    }    

    $.cookie(BOOKMARK_COOKIE_NAME, JSON.stringify(bookmarkedArray), { expires: COOKIE_EXPIRATION_DAYS });
}

function getBookmarkedArray() {
    var cookieValue = $.cookie(BOOKMARK_COOKIE_NAME);
    if (cookieValue === undefined) {
        return [];
    }

    return JSON.parse($.cookie(BOOKMARK_COOKIE_NAME));
}

function cityChanged() {
    var $citySelector = $(this);
    debugger;
    $.cookie(CITY_COOKIE_NAME, $citySelector.val(), { expires: COOKIE_EXPIRATION_DAYS });
    window.location.reload(true);
}