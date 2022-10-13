var swiper = new Swiper(".mySwiper", {
    slidesPerView: 3,
    spaceBetween: 30,
    slidesPerGroup: 1,
    loop: true,
    loopFillGroupWithBlank: true,
    pagination: {
      el: ".swiper-pagination",
      clickable: true,
    },
    navigation: {
      nextEl: ".swiper-button-next",
      prevEl: ".swiper-button-prev",
    },
  });
  // scroll-top
const scrollBtnn = document.querySelector(".btn-scroll-top");
const btnnVisibility = () => {
if (window.scrollY > 400) {
    scrollBtnn.style.visibility = "visible";
} else {
    scrollBtnn.style.visibility = "hidden";
}
};
document.addEventListener("scroll", () => {
btnnVisibility();
});
scrollBtnn.addEventListener("click", (e) => {e.preventDefault();
  window.scroll(0,0);
  });