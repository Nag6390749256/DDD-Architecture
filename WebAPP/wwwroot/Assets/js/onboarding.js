let datePicker = () => {
    $('.datepicker').datepicker({
        dateFormat: 'dd M yy',
        changeYear: true,
        changeMonth: true,
        autoClose: true
    });
}
let loadQualification = () => {
    $.post('/Job/GetMasterQualification').done((response) => {
        $('.qualification').empty();
        $('.qualification').append('<option value="">SELECT STATUS</option>');
        let option = response.map((v, i) => {
            return `<option value="${v.id}">${v.name}</option>`;
        });
        $('.qualification').append(option);
    }).fail((xhr) => {
        Q.notify(-1, 'Server Error!');
        console.error(xhr.responseText);
    });
}
(() => {
    datePicker();
    loadQualification();
})();
var addNewQualification = () => {
    $('#tableQualification tbody').append(`<tr>
       <td>1</td>
       <td>
           <select class="form-control qualification" required>
           <option value="">:: SELECT Qualification ::</option>
           </select>
        </td>
        <td>
            <input type="text" class="form-control" required/>
        </td>
        <td>
            <input type="number" class="form-control" required/>
        </td>
        <td>
            <input type="number" class="form-control" required/>
        </td>
        <td>
            <input type="number" class="form-control" required/>
        </td>
        <td>
            <input type="file" class="form-control" required/>
        </td>
        <td>
            <button class="btn btn-danger btn-sm delete">Delete</button>
        </td>
     </tr>`);
    loadQualification();
}
var addExperience = () => {
    $('#tableexpirence tbody').append(`<tr>
    <td>1</td>
    <td>
        <input type="text" class="form-control" required/>
    </td>
    <td>
        <input type="text" class="form-control" required/>
    </td>
    <td>
        <input type="text" class="form-control datepicker" required/>
    </td>
    <td>
        <input type="text" class="form-control datepicker" required/>
    </td>
    <td>
        <input type="number" class="form-control" required/>
    </td>
    <td>
        <input type="text" class="form-control" required/>
    </td>
    <td>
        <input type="number" class="form-control" required/>
    </td>
     <td>
            <input type="file" class="form-control file" required/>
        </td>
    <td>
        <button class="btn btn-danger btn-sm delete">Delete</button>
    </td>
    </tr>`);
    datePicker();
}
$(document).on('click', '.delete', function () {
    let _currentRow = $(this).closest('tr');
    _currentRow.remove();
});
var saveOnboarding = () => {
    let _valid = valiadteInputs();
    if (!_valid) {
        return false;
    }
    if ($('#tableQualification tbody tr').length == 0) {
        alert('Please add qualification.');
        return false;
    }
    var qualifications = [];
    var experience = [];
    $('#tableQualification tbody tr').each(function () {
        let currentRow = $(this);
        let obj = {
            Qualification: currentRow.find('td:eq(1) select').val(),
            BoardOrUnivercity: currentRow.find('td:eq(2) input').val(),
            YearOfPassing: currentRow.find('td:eq(3) input').val(),
            MaxMarks: currentRow.find('td:eq(4) input').val(),
            ObtainMarks: currentRow.find('td:eq(5) input').val(),
        }
        qualifications.push(obj);
    });
    $('#tableexpirence tbody tr').each(function () {
        let currentRow = $(this);
        let obj2 = {
            Title: currentRow.find('td:eq(1) input').val(),
            CompnayOrSchool: currentRow.find('td:eq(2) input').val(),
            From: currentRow.find('td:eq(3) input').val(),
            To: currentRow.find('td:eq(4) input').val(),
            Salary: currentRow.find('td:eq(5) input').val(),
            ContactPerson: currentRow.find('td:eq(6) input').val(),
            ContactNumber: currentRow.find('td:eq(7) input').val(),
        }
        experience.push(obj2);
    });
    let finalObj = {
        FirstName: $('#FirstName').val(),
        MiddleName: $('#MiddleName').val(),
        LastName: $('#LastName').val(),
        PhoneNumber: $('#PhoneNumber').val(),
        AlternateNumber: $('#AlternateNumber').val(),
        WhatsAppNumber: $('#WhatsAppNumber').val(),
        Email: $('#Email').val(),
        Pincode: $('#Pincode').val(),
        Address: $('#Address').val(),
        Experience: experience,
        Qualifications: qualifications
    }

    $.post('/Onboarding', finalObj).done((response) => {
        alert(response.responseText);
        if (response.statusCode == 1) {
            $('input').val('');
            $('select').val('');
        }
    }).fail((xhr) => {
        alert('Server Error!');
        console.error(xhr.responseText);
    });
}
