$(document).ready(function () {
    var page = window.location.pathname.toString().split("/").pop();

    switch (page.toLowerCase()) {
        case "/":
        case "index": {
            UpdateLinkState("active", "", "", "", "");
            break;
        }
        case "about": {
            UpdateLinkState("", "active", "", "", "");
            break;
        }
        case "courses": {
            UpdateLinkState("", "", "active", "", "", "");
            break;
        }
        case "events": {
            UpdateLinkState("", "", "", "active", "", "");
            break;
        }
        case "contact": {
            UpdateLinkState("", "", "", "", "active");
            break;
        }
        default: {
            UpdateLinkState("active", "", "", "", "");
            break;
        }
    }




});

function UpdateLinkState(index, about, courses, events, contact) {
    $("#lnkIndex").addClass(index);
    $("#lnkAbout").addClass(about);
    $("#lnkCourses").addClass(courses);
    $("#lnkEvents").addClass(events);
    $("#lnkContact").addClass(contact);

}



