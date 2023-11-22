function hidePanel(stylebox) {
    $('#' + stylebox).css('display', 'none');
    return false;
}

function showPanel(stylebox) {
    $('#' + stylebox).css('display', 'block');
}

function openStyleBox(stylebox) {
    $('#' + stylebox).slideDown(1000);
    return false;
}
function closeStyleBox(stylebox) {
    $('#' + stylebox).slideUp(1000);
    return false;
}


