// ========================================================================
// === Ajax based input checker ===========================================
// ========================================================================

function AjaxInputChecker(inputSelector, statusSelector, checkUrl, spinnerImageUrl) {
  this.InputSelector = inputSelector;
  this.StatusSelector = statusSelector;
  this.SpinnerImageUrl = spinnerImageUrl;
  this.LastValue = "";
  this.CheckUrl = checkUrl;

  var inputChecker = this;

  $(inputSelector).blur(function () {
    if (inputChecker.LastValue != $(inputChecker.InputSelector).val()) {
      if (inputChecker.Timer)
        clearTimeout(inputChecker.Timer);
      inputChecker.CheckInput();
    }
  });

  $(inputSelector).keyup(function () {
    if (inputChecker.Timer)
      clearTimeout(inputChecker.Timer);
    if (inputChecker.LastValue != $(inputChecker.InputSelector).val()) {
      var statusElement = $(inputChecker.StatusSelector);
      statusElement.html("");
      inputChecker.Timer = setTimeout(inputChecker.CheckInput, 1000);
    }
  })


  this.CheckInput = function () {
    var value = $(inputChecker.InputSelector).val();
    if (value == "")
      return;
    inputChecker.LastValue = value;
    var statusElement = $(inputChecker.StatusSelector);
    statusElement.html("<img src=\"" + inputChecker.SpinnerImageUrl + "\" />");
    $.get(inputChecker.CheckUrl, { value: value },
    function (data) {
      var statusElement = $(inputChecker.StatusSelector);
      var className = (data.Ok ? "ok" : "fail");
      if (!data.ok || value == data.CheckedValue) {
        var html = "<span class=\"{0}\">{1}</span>".format(className, data.Message);
        statusElement.html(html);
      }
      else {
        statusElement.html('');
        this.CheckInput();
      }
    });
  }
}


// ========================================================================
// === Add password policy validator ======================================
// ========================================================================

jQuery.validator.unobtrusive.adapters.add("passwordpolicy", ["policyexpr"], function (options) {
  options.rules["passwordpolicy"] = [options.params.policyexpr];
  options.messages["passwordpolicy"] = options.message;
});

jQuery.validator.addMethod('passwordpolicy', function (value, element, param) {
  var expr = new RegExp(param[0]);
  return expr.test(value);
});
