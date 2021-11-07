'use strict';
var DATA_ID = 'id',
    COOKIE_EXPIRATION_DAYS = 365,
    BOOKMARK_COOKIE_NAME,
    BOOKMARK_LINK_SELECTOR = '.js-bookmark',
    BOOKMARKED_CLASS = 'bookmarked',
    CITY_COOKIE_NAME,
    ONLY_WITH_MENU_COOKIE_NAME,
    CITY_SELECTOR = '.city',
    ONLY_WITH_MENU_SWITCH = '.onlyWithMenu';

function init(bookmarkCookieName, cityCookieName, onlyWithMenuCookieName) {
    BOOKMARK_COOKIE_NAME = bookmarkCookieName;
    CITY_COOKIE_NAME = cityCookieName;
    ONLY_WITH_MENU_COOKIE_NAME = onlyWithMenuCookieName;
    registerHandlers();
}

function registerHandlers() {
    $(BOOKMARK_LINK_SELECTOR).on('click', function () {
        var $link = $(this);
        var id = $link.data(DATA_ID);
        var bookmarked = addOrRemoveBookmarked(id);

        $link.toggleClass(BOOKMARKED_CLASS, bookmarked.includes(id));
        setBookmarkCookie(bookmarked);
    });
    $(CITY_SELECTOR).on('change', cityChanged);
    $(ONLY_WITH_MENU_SWITCH).on('change', onlyWithMenuChanged);
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
    $.cookie(CITY_COOKIE_NAME, $citySelector.val(), { expires: COOKIE_EXPIRATION_DAYS });
    reloadPage();
}

function onlyWithMenuChanged() {
    var $onlyWithMenuSelector = $(this);
    $.cookie(ONLY_WITH_MENU_COOKIE_NAME, $onlyWithMenuSelector.prop('checked'), { expires: COOKIE_EXPIRATION_DAYS });
    reloadPage();
}

function reloadPage() {
    window.location.reload(true);
}