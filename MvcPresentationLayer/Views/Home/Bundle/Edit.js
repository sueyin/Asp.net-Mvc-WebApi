
$(document).ready(function () {
    //initialise 
    $("input").prop('disabled', true);
    var confirmButton = $("button[class=confirm_button]");
    confirmButton.hide();

    $("button[class=edit_button]").click(function () {
        // ID of the employee to be edited  
        var t = $(this).attr('id');

        //change label to input
        var nameLabel = $("label[id=Name_" + t + "]");

        $("input[id=Name_" + t + "]").prop('disabled', false);
        $("input[id=ContactNumber_" + t + "]").prop('disabled', false);
        $("input[id=Address_" + t + "]").prop('disabled', false);

        var editButton = $("button[class=edit_button]");
        editButton.hide();
        var removeButton = $("td[class=remove_button]");
        removeButton.hide();

        //show edit row's confirm button 
        var editConfirmButton = $("button[id=Confirm_" + t + "]");
        editConfirmButton.show();

        editConfirmButton.click({ ID: "" + t }, ConfirmChange);

        function ConfirmChange(event) {
            var updatedEmployee = new Object();
            updatedEmployee.ID = t;
            updatedEmployee.Name = $("input[id=Name_" + t + "]").val();
            updatedEmployee.ContactNumber = $("input[id=ContactNumber_" + t + "]").val();
            updatedEmployee.Address = $("input[id=Address_" + t + "]").val();
            var url = 'http://127.0.0.1:8080/api/employee/' + t.toString();
            $.ajax({
                url: url,
                type: 'PUT',
                data: JSON.stringify(updatedEmployee),
                contentType: "application/json;charset=utf-8",
                success: function (data, textStatus, xhr) {
                    console.log(data);
                    $("input[id=Name_" + t + "]").prop('disabled', true);
                    $("input[id=ContactNumber_" + t + "]").prop('disabled', true);
                    $("input[id=Address_" + t + "]").prop('disabled', true);
                    editConfirmButton.hide();
                    editButton.show();
                    removeButton.show();
                },
                error: function (xhr, textStatus, errorThrown) {
                    alert("fail");
                    console.log('Error in Operation');
                }
            });
        }
    });
});
