use num_bigint::BigUint;
use num_traits::One;
use std::time::Instant;

use std::ffi::CString;
use std::os::raw::c_char;

// Export the Rust BigInt factorial function for use in .NET via FFI
#[no_mangle]
pub extern "C" fn factorial_big(n: u64) -> *mut c_char {
    let start = Instant::now();
    let result = big_factorial(n);
    let duration = start.elapsed();
    println!("Rust: Execution time: {:?}", duration);

    let c_string = CString::new(result.to_string()).unwrap();

    // Return a pointer to the C-style string
    c_string.into_raw()
}

// Helper function for factorial with BigUint
fn big_factorial(n: u64) -> BigUint {
    let mut result = BigUint::one();
    for i in 1..=n {
        result *= i;
    }
    result
}
