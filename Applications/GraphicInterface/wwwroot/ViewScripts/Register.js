function FormatString(str, ...val) {
    for (let index = 0; index < val.length; index++) {
        str = str.replaceAll(`{${index}}`, val[index]);
    }
    return str;
}

let currentItems = [];

let txtSearch;
let txtRequestedMoney;
let txtMoney;
let txtChange;
let txtCount;
let txtBarcode;
let btnScan;
let tblSearch;
let tblItems;
let itemTemplate;
let currentItemTemplate;
let btnSave;

const assignControls = () => {
    itemTemplate = $('#searchItemTemplate');
    tblSearch = $('#tblSearch').find('tbody');
    txtSearch = $('#txtSearch')
    btnScan = $('#btnScan')
    txtCount = $('[name="txtCount"]')
    txtBarcode = $('[name="txtBarcode"]')
    tblItems = $('#tblCurrentItems').find('tbody');
    currentItemTemplate = $('#leftItemTemplate');
    txtRequestedMoney = $('#txtRequestedMoney');
    txtMoney = $('#txtMoney');
    txtChange = $('#txtChange');
    btnSave = $('#btnSave');
}

const initControls = () => {
    txtSearch.on('change', () => {
        search(txtSearch.val());
    });
    
    btnScan.click(() => {
        const amount = txtCount.val() ? txtCount.val() : 1;
        scan(txtBarcode.val(), amount);
    });

    btnSave.click(() => {
        save();
    });
    
    txtRequestedMoney.change(() => {
        const cost = +txtRequestedMoney.val();
        const curr = +txtMoney.val();
        txtChange.val(curr - cost);
        debugger
        if((curr - cost) < 0) {
            btnSave.addClass('d-none');
        } else {
            btnSave.removeClass('d-none')
        }
    });
    
    txtMoney.change(() => txtRequestedMoney.change());
}

const search = (text) => {
    $.ajax({
        url: '/Product/GetProducts',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        method: 'GET',
        data: {
            text: text
        }
    }).then((result) => {
        tblSearch.empty();
        for(let i =0; i< result.length;i++) {
            const item = result[i];
            tblSearch.append(FormatString(itemTemplate.html(), item.Name, item.Barcode, item.IsCountable, item.Price))
        }
    });
}

const removeItem = (barcode) => {
    currentItems = currentItems.filter(x => x.Barcode != barcode);
    redrawCurrentItems();
}

const fillScan = (barcode, countable) => {
    if(countable) txtCount.val('1');
    else txtCount.val('');
    
    txtBarcode.val(barcode);
}

const redrawCurrentItems = () => {
    tblItems.empty();
    currentItems.forEach(x => {
        tblItems.append(FormatString(currentItemTemplate.html(), x.Barcode, x.ProductName, x.Count));
    });
    let sum = 0;
    currentItems.forEach(x => sum += x.Count * x.PricePerOne);
    txtRequestedMoney.val(sum).change();
}

const scan = (barcode, amount) => {
    $.ajax({
        url: '/Register/Scan',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        method: 'GET',
        data: {
            barcode: barcode
        }
    }).then((result) => {
        if(result) {
            const index=  currentItems.findIndex(x => x.Barcode == barcode);
            if(index < 0) {
                currentItems.push({
                    Barcode: result.Barcode,
                    ProductName: result.Name,
                    Count: +amount,
                    ProductId: result.Id,
                    PricePerOne: result.Price
                });
            } else {
                currentItems[index].Count += +amount;
            }
            redrawCurrentItems();
            txtBarcode.val('');
            txtCount.val('');
        }
    });
}

const save = () => {
    $.ajax({
        url: '/Register/Sale',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        method: 'POST',
        data: JSON.stringify(currentItems)
    }).then(() => {
        window.location.reload();
    });
}

$(document).ready(() => {
    assignControls();
    initControls();
})