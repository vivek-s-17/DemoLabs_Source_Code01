console.info("------------ DEMO: Using the `typeof` Operator");


let x = null;
console.log(typeof x);                      // "object"



let testLet;                                // declared but not assigned. So it is "undefined"

if (typeof testLet === "undefined") {
    console.log("testLet is undefined");
} else {
    console.log("testLet is defined");
}



/***********
    The typeof operator is particularly useful for checking undeclared variables without throwing a ReferenceError.
    A direct comparison 
        unDeclaredVariable === undefined  OR    unDeclaredVariable == undefined
        unDeclaredVariable === null       OR    unDeclaredVariable == null
    would cause a ReferenceError in the below shown example.
 ***/

// 'unDeclaredVariable' has not been declared anywhere
if (typeof unDeclaredVariable === "undefined") {
    console.log("undeclaredVariable is undeclared or has a value of undefined");
}

