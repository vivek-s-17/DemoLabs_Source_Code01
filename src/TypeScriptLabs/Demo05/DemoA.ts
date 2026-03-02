console.info("------------ DEMO: Check for FALSE");


export { };


let values = [false, 0, "", null, undefined, NaN];


for (let v of values) {
    if (v) {
        console.log(v, "is truthy");
    } else {
        console.log(v, "is falsy");
    }
}



// This can be a really useful feature.
let count = 0;
let value = count || 10;
console.log(value);                 // 10  <- because 0 is false!



console.log(Boolean(""));           // false
console.log(Boolean("hello"));      // true
console.log(Boolean(0));            // false
console.log(Boolean(100));          // true

