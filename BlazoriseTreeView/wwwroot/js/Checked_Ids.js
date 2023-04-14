
export function checkChange() {

    const checkboxes = document.querySelectorAll('.check');
    const difficultyVals = [...checkboxes].map(e => ({ el: e, val: e.value }));


    
    const getActiveVals = () => difficultyVals.filter(({ el }) => el.checked).map(({ val }) => val);

    console.log("getActiveVals" + getActiveVals());
  
    return getActiveVals();
  
}

