$.validator.addMethod('filesize', function (value, element, par) {
    return this.optional(element) || element.files[0].size <= par;
});

$(document).ready(function () {
    $('#Cover').on('change', function () {
        $('.cover-preview').attr('src', window.URL.createObjectURL(this.files[0])).removeClass('d-none');
    });
});