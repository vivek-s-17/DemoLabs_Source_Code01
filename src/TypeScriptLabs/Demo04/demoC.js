console.info("------------ DEMO: Check for `null` or `undefined`");


let testLet;                     // declared but not assigned. So it is "undefined"

if (testLet == null) {
    console.log("testLet == null => true");
} else {
    console.log("testLet == null => false");
}

if (testLet === null) {
    console.log("testLet === null => true");
} else {
    console.log("testLet === null => false");
}


if (testLet == undefined) {
    console.log("testLet == undefined => true - loose equality");
} else {
    console.log("testLet == undefined => false");
}

if (testLet === undefined) {
    console.log("testLet === undefined => true");
} else {
    console.log("testLet === undefined => false");
}
