console.info("------------ DEMO: Hoisting");

export { };


var testVar;
console.log(testVar);   // undefined
testVar = 100;
console.log(testVar);   // 100


let testLet;
console.log(testLet);   // undefined
testLet = 10;
console.log(testLet);   // 10
