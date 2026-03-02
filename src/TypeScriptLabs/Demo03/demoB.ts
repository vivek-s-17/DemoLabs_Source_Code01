console.info("------------ DEMO: Re-declaration");

export { };

var x = 10;
var x = 20;                 // allowed (hence, problematic.  Hence, use of "var" is discouraged)

let y = 30;
// let y = 40;              // Error

const z = 50;
// const z = 60;           // Error 
