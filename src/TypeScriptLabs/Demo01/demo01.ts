// First example of TypeScript

/**********
    NOTE: 
    Commenting/Removing the line:
        export { }; 
    results in a compilation error by TypeScript.
    
    Reason "name" is already defined in inside the `lib.dom.d.ts` file, something like:
        declare const name: void;
    This is a global variable defined in the global scope.

    SOLUTION:
    Mark this file as a Module, by adding the `export {}` directive,
    so that it can have its own scope.
 */

// Makes this file a module
export { };         


// Declaration without initialization
let name: string;

// Declaration with initialization
let age: number = 25;

// Assigning value later
name = "Manoj Kumar Sharma";

// Simple console logs
console.log(name);
console.log(age);

// Template literal
console.log(`Name: ${name}, Age: ${age}`);
