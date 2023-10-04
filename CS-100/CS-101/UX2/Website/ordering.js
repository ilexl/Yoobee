const orderID = "currentOrder";
const holderId = "#ItemHolderConfirm";

const FoodName = ["Edamame", "Seaweed Salad", "Kimichi", "Tonkotsu Ramen", "Tori Ramen", "Spicy Miso Ramen", "Pork Rice Bowl", "Kaarange Rice Bowl", "Tofu Rice Bowl"];
const FoodPrice = [5, 6, 5.5, 12, 12.5, 12.5, 10, 10.5, 11];

const SYMBOLSPLIT = '☒';

const DELIVERYPRICE = "$5.00";

function getCookie(name) {
  // Split cookie string and get all individual name=value pairs in an array
  var cookieArr = document.cookie.split(";");
  
  // Loop through the array elements
  for(var i = 0; i < cookieArr.length; i++) {
      var cookiePair = cookieArr[i].split("=");
      
      /* Removing whitespace at the beginning of the cookie name
      and compare it with the given string */
      if(name == cookiePair[0].trim()) {
          // Decode the cookie value and return
          return decodeURIComponent(cookiePair[1]);
      }
  }
  
  // Return null if not found
  return null;
}

function setCookie(name, value, daysToLive = 365) {
  // Encode value in order to escape semicolons, commas, and whitespace
  var cookie = name + "=" + encodeURIComponent(value);
  
  if (typeof daysToLive === "number") {
      /* Sets the max-age attribute so that the cookie expires
      after the specified number of days */
      cookie += "; max-age=" + (daysToLive*24*60*60);
  }
      
  document.cookie = cookie;
}

function addItemToCart(item){
  alert(FoodName[item] +  " added to cart!");
  console.log(FoodName[item], "added to cart!");

  if(getCookie(orderID) == ""){
    setCookie(orderID, item.toString());
  }
  else{
    setCookie(orderID, getCookie(orderID) + SYMBOLSPLIT + item.toString());
  }
}

function completeOrder(){
  if(getCookie(orderID) == ""){
    alert("No items in cart...");
  }
  else{
    window.open("./CompleteOrder.html","_self");
  }
}

function DeleteOrder(){
  setCookie(orderID, "");
}

function UpdatePrices(){
  let items_final = holder.getElementsByClassName("item");
  // console.log(items_final);

  for(let i = 0; i < items_final.length; i++){
    let holder_item = items_final[i];
    // console.log(holder_item);

    let item_holder_R_array = holder_item.getElementsByClassName("item-holder-R");
    let item_holder_R = item_holder_R_array[0];

    // Unit value (from obj)

    let unit_price_array = holder_item.getElementsByClassName("unit-price");
    let unit_total = unit_price_array[0];
    let unit_text_obj = unit_total.childNodes;
    let unit_text = unit_text_obj[0];
    let unit_value_str = unit_text.nodeValue;
    unit_value_str = unit_value_str.replace("$", "");
    let unit_value =  parseFloat(unit_value_str);

    // Amount value (from obj)

    let amount_obj = holder_item.getElementsByTagName("input");
    let amount_input_box = amount_obj[0];
    let amount = parseFloat(amount_input_box.value);
    if(amount < 0){
      amount = 0;
      amount_input_box.value = 0;
    }

    // Total value (from obj)

    let item_price_array = holder_item.getElementsByClassName("item-price");
    let item_total = item_price_array[0];
    let item_total_text_obj = item_total.childNodes;
    let item_total_text = item_total_text_obj[0];
    let str_new = "$" + (unit_value * amount).toFixed(2);

    item_total_text.nodeValue = str_new;
  }
  UpdateDetails();
}

if(getCookie(orderID) == null){
  setCookie(orderID, "");
}

const holder = document.querySelector(holderId);

let items_all = [];

if(holder != null){
  let itemsRaw = getCookie(orderID);
  let items = itemsRaw.split(SYMBOLSPLIT);
  
  for(let item in items){
    let item_holder = document.createElement("div");
    item_holder.className = "item";


    holder.appendChild(item_holder);
    items_all.push(item_holder);
    
    let temp;

    let image;
    let name;
    let inputbox;
    let price;
    let total;

    let holderL;
    let holderR;

    holderL = document.createElement("div");
    holderR = document.createElement("div");

    holderL.className = "item-holder-L";
    holderR.className = "item-holder-R";

    item_holder.appendChild(holderL);
    item_holder.appendChild(holderR);
  
    image = document.createElement("img");
    temp = parseInt(parseInt(items[item])) + 1;
    image.src="./images/menu/" + temp.toString() + ".png";
    image.alt = "menu image";
    holderL.appendChild(image);

    temp = null;

    name = document.createElement("h2");
    temp = document.createTextNode(FoodName[parseInt(items[item])]);
    name.appendChild(temp);
    holderL.appendChild(name);

    temp = null;

    inputbox = document.createElement("input");
    inputbox.value = 1;
    inputbox.name = items[item];
    inputbox.onchange = UpdatePrices;
    holderR.appendChild(inputbox);

    temp = null;

    price = document.createElement("h2");
    temp = document.createTextNode("$" + FoodPrice[parseInt(items[item])].toFixed(2));
    price.appendChild(temp);
    price.className = "unit-price";
    holderR.appendChild(price);

    temp = null;

    total = document.createElement("h2");
    temp = document.createTextNode("$" + FoodPrice[parseInt(items[item])]);
    total.appendChild(temp);
    total.className = "item-price";
    holderR.appendChild(total);
  }

  // console.log(items_all);

  let removed = false;
  for(let item in items_all){
    
    
    // console.log(item);
    for(let subitem in items_all){
      
      // console.log(items_all[item]);
      // console.log(items_all[subitem]);
      // console.log(items_all[item] == items_all[subitem]);
      if(items_all[item] != items_all[subitem]){
        // Do check

        // get image number
        let imgs = items_all[item].getElementsByTagName('img');
        let srcName = imgs[0].src;
        srcName = srcName.replace(".png", "");

        let lastTwo = srcName.substr(srcName.length - 2);
        srcName = lastTwo.replace("/", "");

        // get image number
        let imgsO = items_all[subitem].getElementsByTagName('img');
        let srcNameO = imgsO[0].src;
        srcNameO = srcNameO.replace(".png", "");
        
        let lastTwoO = srcNameO.substr(srcName.length - 2);
        srcNameO = lastTwoO.replace("/", "");

        // console.log(imgsO);
        // console.log(imgs);

        // console.log(srcName);
        // console.log(srcNameO);


        if(parseInt(srcName) == parseInt(srcNameO)  && item < subitem){
          // console.log(srcName);

          // Todo
          // Add amount to first item

          let item_add = items_all[item].getElementsByTagName('input');
          // console.log(item_add[0]);
          item_add[0].value = parseInt(item_add[0].value) + 1;
          // console.log(item_add[0].value);

          // Delete Second Item (:
          let item_remove = items_all[subitem];

          



          item_remove.remove();
          removed = true;
        }

        
      }
    }
  }

  // update amount box on last to amount (:
  let items_final = holder.getElementsByClassName("item");
  // console.log(items_final);

  for(let i = 0; i < items_final.length; i++){
    let holder_item = items_final[i];
    // console.log(holder_item);

    let item_holder_R_array = holder_item.getElementsByClassName("item-holder-R");
    let item_holder_R = item_holder_R_array[0];

    // Unit value (from obj)

    let unit_price_array = holder_item.getElementsByClassName("item-price");
    let unit_total = unit_price_array[0];
    let unit_text_obj = unit_total.childNodes;
    let unit_text = unit_text_obj[0];
    let unit_value_str = unit_text.nodeValue;
    unit_value_str = unit_value_str.replace("$", "");
    let unit_value =  parseFloat(unit_value_str);

    // Amount value (from obj)

    let amount_obj = holder_item.getElementsByTagName("input");
    let amount_input_box = amount_obj[0];
    let amount = parseFloat(amount_input_box.value);
    if(amount < 0){
      amount = 0;
      amount_input_box.value = 0;
    }

    // Total value (from obj)

    let item_price_array = holder_item.getElementsByClassName("item-price");
    let item_total = item_price_array[0];
    let item_total_text_obj = item_total.childNodes;
    let item_total_text = item_total_text_obj[0];
    let str_new = "$" + (unit_value * amount).toFixed(2);

    item_total_text.nodeValue = str_new;

    console.log();
  }

}


let SummarySubtotalH3 = document.getElementById("SubtotalH3");
let SummaryDeliveryH3 = document.getElementById("DeliveryH3");
let SummaryTaxH3 = document.getElementById("TaxH3");
let SummaryTotalH3 = document.getElementById("TotalH3");

let deliveryAddressInput = document.getElementById("deliveryAddressInput");
let deliverySuburbInput = document.getElementById("deliverySuburbInput");
let deliveryPostcodeInput = document.getElementById("deliveryPostcodeInput");
let deliveryPhonenumberInput = document.getElementById("phoneNumberInput");

let paymentCardnumberInput = document.getElementById("paymentCardnumberInput");
let paymentExpirydateInput = document.getElementById("paymentExpirydateInput");
let paymentCvvInput = document.getElementById("paymentCvvInput");
let paymentCardholdernameInput = document.getElementById("paymentCardholdernameInput");

function AddressChanged(){
  if((deliveryAddressInput.value != null && deliveryAddressInput.value != "") || (deliverySuburbInput.value != null && deliverySuburbInput.value != "") || (deliveryPostcodeInput.value != null && deliveryPostcodeInput.value != "") || (deliveryPhonenumberInput.value != null && deliveryPhonenumberInput.value != "")){
    AddressFilledIn = true;
  }
  else{
    AddressFilledIn = false;
  }
  //console.log(AddressFilledIn);
  UpdateDetails();
}

function UpdateDetails(){
  if(AddressFilledIn){
    SummaryDeliveryH3.innerHTML = DELIVERYPRICE;
  }
  else{
    SummaryDeliveryH3.innerHTML = "$0.00";
  }

  let items_final = document.getElementsByClassName("item");
  let subtotalPrice = 0;

  // console.log(items_final);
  // console.log(items_final.length);

  for(let i = 0; i < items_final.length;i++){
    let holder_item = items_final[i];
    // console.log(holder_item);
    let item_price_array = holder_item.getElementsByClassName("item-price");
    let item_total = item_price_array[0];
    let item_total_price_string = item_total.innerHTML;
    // console.log(item_total_price_string);
    item_total_price_string = item_total_price_string.replace("$", "");
    let item_final_price = parseFloat(item_total_price_string);
    subtotalPrice += item_final_price;
    
  }
  SummarySubtotalH3.innerHTML = "$" + subtotalPrice.toFixed(2);

  // SummaryDeliveryH3.innerHTML;
  // SummaryTaxH3.innerHTML;
  // subtotalPrice;
  let deliveryTxt = SummaryDeliveryH3.innerHTML;
  deliveryTxt = deliveryTxt.replace("$", "");
  let deliveryPrice = parseFloat(deliveryTxt);

  let midTotal = subtotalPrice + deliveryPrice;
  let tax = midTotal * 0.15;

  SummaryTaxH3.innerHTML = "$" + tax.toFixed(2);

  let totalPriceFinal = midTotal + tax;

  SummaryTotalH3.innerHTML = "$" + totalPriceFinal.toFixed(2);
}


let AddressFilledIn = false;
AddressChanged();

function EndOrder(){
  if(paymentCardnumberInput.value != "" && paymentExpirydateInput.value != "" && paymentCvvInput.value != "" && paymentCardholdernameInput.value != ""){
    alert("Order Complete!\nThank you for shopping at The Ramen Shop (:");
    DeleteOrder();
    window.open("./index.html","_self");
  }
  else{
    alert("Please enter Payment...");
  }
}