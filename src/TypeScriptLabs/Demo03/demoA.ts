console.info("------------ DEMO: Basic Declaration Differences");

export { };

let a = 10;
a = 20;             // allowed

const b = 30;
// b = 40;          // error

var c = 50;
c = 60;             // allowed

console.log(a, b, c);
