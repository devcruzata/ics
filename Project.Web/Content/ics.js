var ICS = {};

ICS.form = function() {

    var that = {};

    that.init = function() {
        that.initClicks();
    };

    that.initClicks = function() {
        $('#clear').click(function() {
            that.clearForm();
        });

        $('#submit').click(function() {

            if (that.validate() === false) {
                return;
            }

            $.getJSON(
                'https://www.icsleads.com/Api/AddLead/?jsoncallback=?',
                {
                    source: $('#source').val(),
                    originator: $('#originator').val(),
                    agentCode: $('#agentCode').val(),
                    returnType: 'jsonp',
                    businessName: $('#businessName').val(),
                    contactName: $('#contactName').val(),
                    phone: $('#phone').val(),
                    phoneAlt: $('#phoneAlt').val(),
                    email: $('#email').val(),
                    comments: $('#comments').val()
                },
                function(data) {
                    if (data.Success) {

                        var source = $('#source').val();
                        var originator = $('#originator').val();
                        var agentCode = $('#agentCode').val();
                        var returnType = $('#returnType').val();
                        var businessName = $('#businessName').val();
                        var contactName = $('#contactName').val();
                        var phone = $('#phone').val();
                        var phoneAlt = $('#phoneAlt').val();
                        var email = $('#email').val();
                        var comments = $('#comments').val();

                        //that.clearForm();
                        $('<span id="valsum" class="success">Lead was successfully submitted.</span>').appendTo('#message');

                        var info = '<ul class="success">';
                        info += '<li>Lead Id: ' + data.LeadId + '</li>';
                        info += businessName === '' ? '' : ('<li>Business Name: ' + businessName + '</li>');
                        info += contactName === '' ? '' : ('<li>Contact Name: ' + contactName + '</li>');
                        info += phone === '' ? '' : ('<li>Phone: ' + phone + '</li>');
                        info += phoneAlt === '' ? '' : ('<li>Secondary Phone: ' + phoneAlt + '</li>');
                        info += email === '' ? '' : ('<li>Email Address: ' + email + '</li>');
                        info += comments === '' ? '' : ('<li>Comments: ' + comments + '</li>');
                        info += '</ul>';

                        $(info).appendTo('#message');
                    } else {
                        that.clearMessage();
                        $('<span id="valsum" class="validationsummary">Submit was unsuccessful. Please correct the errors and try again.</span>').appendTo('#message');
                        $('<ul id="vallist" class="validationsummary"></ul>').appendTo('#message');
                        $.each(data.Errors, function(i, item) {
                            $('<li></li>').html(item.Message).appendTo('#vallist');

                        });
                    }
                }
            );
        });
    };

    that.validate = function() {

        var validated = true;

        that.clearMessage();

        var valList = $('<ul id="vallist" class="validationsummary"></ul>');

        var source = $('#source').val();
        if (source === '') {
            $('<li></li>').html('Source cannot be empty.').appendTo(valList);
            validated = false;
        }

        var originator = $('#originator').val();
        if (originator === '') {
            $('<li></li>').html('Originator cannot be empty.').appendTo(valList);
            validated = false;
        }

        var contactName = $('#contactName').val();
        if (contactName === '') {
            $('<li></li>').html('Contact Name cannot be empty.').appendTo(valList);
            validated = false;
        }

        var phone = $('#phone').val();
        if (phone === '') {
            $('<li></li>').html('Phone Number cannot be empty.').appendTo(valList);
            validated = false;
        }

        if (validated === false) {
            $('<span id="valsum" class="validationsummary">Submit was unsuccessful. Please correct the errors and try again.</span>').appendTo('#message');
            $(valList).appendTo('#message');
        }

        return validated;
    };

    that.clearForm = function() {
        $('#leadform').find("input").val("");
        $('#leadform').find("select").val("");
        $('#message').html("");
    };

    that.clearMessage = function() {
        $('#message').html("");
    };


    return that;

} ();