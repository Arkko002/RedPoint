function htmlEncode(value: any) {
    return $("<div />").text(value).html();
}
