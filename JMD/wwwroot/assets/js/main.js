const bars=document.querySelector(".bars")
const navbar=document.querySelector(".navbar")
const remove=document.querySelector(".navbar svg")
console.log(bars);
bars.addEventListener("click",()=>{
   navbar.classList.add("navbar_active")
})

remove.addEventListener("click",()=>{
     navbar.classList.remove("navbar_active")
  })



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
 
 document.addEventListener("click", function (event) {
   const option = event.target.closest(".option");
   if (option) {
     const allOptions = document.querySelectorAll(".option");
     allOptions.forEach(function (opt) {
       opt.classList.remove("active");
     });
     option.classList.add("active");
     option.closest(".custom-select").querySelector(".selected-option").textContent = option.textContent;
   }
 });
 