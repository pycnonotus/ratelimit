const BASE_URL = "https://localhost:7132";
const IS_PRIME_END_POINT = BASE_URL+"/api/is-prime-number";

const KEY_SPAN = document.getElementById("key");
const TOKEN_INPUT = document.getElementById("token");
const NUMBER_INPUT = document.getElementById("number-to-test");
const END_POINT_RESULT_ELEMENT = document.getElementById("end-point-result");



onGenerateToken();


function onGenerateToken(){
    const token = generateToken();
    KEY_SPAN.innerHTML = token;
    TOKEN_INPUT.value = token;

}

function generateToken(){
    
    return "Bearer token"+Math.random()+"";
}

async function onIsPrimeNumber(){
    var number = parseInt(NUMBER_INPUT.value);
    if(number === NaN) return; //TODO add error message

    try{
        var result = await checkAgainstEndApiIsPrimeNumber(number);
        END_POINT_RESULT_ELEMENT.innerHTML = result.toString();
    }catch(error){
        console.log(error);
    }
}

async function checkAgainstEndApiIsPrimeNumber(number){
    var apiResponse = await fetch(`${IS_PRIME_END_POINT}/${number}`, { 
        method: 'GET',
         headers: { 
            'Content-Type': 'application/json',
            'Authorization': TOKEN_INPUT.value
        }
        });
    return apiResponse.text();
}
