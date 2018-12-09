function htmlEncode(value: any) {
    var encodedValue = $("<div />").text(value).html();
    return encodedValue;
}
