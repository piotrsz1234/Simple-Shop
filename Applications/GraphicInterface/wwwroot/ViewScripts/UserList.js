function FormatString(str, ...val) {
    for (let index = 0; index < val.length; index++) {
        str = str.replaceAll(`{${index}}`, val[index]);
    }
    return str;
}

let tblUsers;
let itemTemplate;
let txtText;

const init = () => {
    assignControls();
    initControls();
    loadData('');
};

const assignControls = () => {
    tblUsers = $('#tblUsers').find('tbody');
    itemTemplate = $('#itemTemplate');
    txtText = $('#txtText');
};

const initControls = () => {
    txtText.on('input', () => {
        loadData(txtText.val());
    });
};

const loadData = (text) => {
    $.ajax({
        url: '/User/GetUsers',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        method: 'GET',
        data: {
            text: text
        }
    }).then((result) => {
        tblUsers.empty();
        for(let i =0; i< result.length;i++) {
            const item = result[i];
            console.log(itemTemplate)
            console.log(itemTemplate.html())
            console.log(FormatString(itemTemplate.html(), item.Id, item.FirstName, item.LastName, (item.IsAdmin ? 'Admin' : 'Normal user')))
            tblUsers.append(FormatString(itemTemplate.html(), item.Id, item.FirstName, item.LastName, (item.IsAdmin ? 'Admin' : 'Normal user')))
        }
    });
}
$(document).ready(() => {
    init();
});
