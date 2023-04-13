var checkedArray = [];
export function checkChange(node_id) {

    //checkedArray.push(node_id);
    //var result = [];
    //$.each(checkedArray, function (i, e) {
    //    if ($.inArray(e, result) == -1) result.push(e);
    //});
 
    ///*if (checkedArray.indexOf(node_id) === -1) checkedArray.push(node_id);*/
    //console.log("checkedArray " + result);

    //var inputs = document.getElementsByTagName(node_id).value;
   
    ////var checks = [];
    ////for (var i = 0; i < inputs.length; i++) {
    ////    if (inputs[i].type == "checkbox" && inputs[i].checked) {
    ////        checks.push(inputs[i]);
    ////    }
    ////}

    let checkboxes = document.querySelectorAll('.check:checked');
    checkedArray.push(checkboxes);
    for (var i in checkboxes) {
        console.log("ids" + i.id);
    }
    console.log(checkboxes);
}

