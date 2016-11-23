$(function () {
    $(".date-picker").datetimepicker({
        minView: "month",
        format: "yyyy-mm-dd",
        autoclose: true,
        todayBtn: true,
        language: 'zh-CN',
        pickerPosition: "bottom-left"
    });
    $(".datetime-picker").datetimepicker({
        format: "yyyy-mm-dd hh:ii:ss",
        autoclose: true,
        todayBtn: true,
        language: 'zh-CN',
        pickerPosition: "bottom-left"
    });
    $(".formated-datetime-picker").each(function () {
        var target = $(this);
        var format = target.data("format");
        target.datetimepicker({
            minView: "month",
            format: $(this).data("format"),
            autoclose: true,
            todayBtn: true,
            language: 'zh-CN',
            pickerPosition: "bottom-left"
        });
    })
});