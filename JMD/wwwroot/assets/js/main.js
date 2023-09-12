

// header responsive navbar start 
const bars=document.querySelector(".bars")
const navbar=document.querySelector(".navbar")
const remove=document.querySelector(".navbar svg")

bars.addEventListener("click",()=>{
   navbar.classList.add("navbar_active")
})

remove.addEventListener("click",()=>{
     navbar.classList.remove("navbar_active")
  })
// header responsive navbar end



// header language start
  document.addEventListener("click", function (event) {
   if (event.target.closest(".selected-option")) {
     const select = event.target.closest(".custom-select");
     select.classList.toggle("active");
   
   } else {
     const allCustomSelects = document.querySelectorAll(".custom-select");
     allCustomSelects.forEach(function (select) {
       select.classList.remove("active");
     });
   }
 });
 // header language end




// contact start
document.addEventListener("click", function(event) {
  const option = event.target.closest(".option");
  if (option) {
      const customSelect = option.closest(".custom-select");
      const allOptions = customSelect.querySelectorAll(".option");
      allOptions.forEach(function(opt) {
          opt.classList.remove("active");
      });
      option.classList.add("active");

      const selectedText = option.textContent;

      customSelect.querySelector(".selected-option").textContent =selectedText;
      console.log(customSelect.querySelector(".input_value").value = selectedText); 
  }
});
// contact end
 


// order start
//const form_botton=document.querySelector(".section_order .order_boxes .form_botton")
//const order_overlay=document.querySelector(".section_order .order_boxes .order_overlay")
//const overlay_remove=document.querySelector(".section_order .order_boxes .order_overlay .overlay_remove")


//form_botton.addEventListener("click",()=>{
//  order_overlay.classList.add("order_overlay_active")
//})

//overlay_remove.addEventListener("click",()=>{
//  order_overlay.classList.remove("order_overlay_active")
//})
// order end