// Search

var search = document.querySelector("#search");
var txtSearch = document.querySelector(".searchProductContainer");

search.addEventListener('click',(e) => {
	e.preventDefault();
	txtSearch.classList.toggle('hide');
});

// choose img in product
const imgs = document.querySelectorAll('.img-select a');
const imgBtns = [...imgs];
let imgId = 1;

imgBtns.forEach((imgItem) => {
    imgItem.addEventListener('click', (e) => {
        e.preventDefault();
        imgId = imgItem.dataset.id;
        slideImage();
    });
});

function slideImage(){
    const displayWidth = document.querySelector('.img-showcase img:first-child').clientWidth;

    document.querySelector('.img-showcase').style.transform = `translateX(${- (imgId - 1) * displayWidth}px)`;
}

window.addEventListener('resize', slideImage);

//------------------------- Viết nhận xét----------------------------- 

$('#exampleModal').on('show.bs.modal', function (event) {
  var button = $(event.relatedTarget) 
  var recipient = button.data('whatever') 
  var modal = $(this)
  modal.find('.modal-title').text(recipient)
  modal.find('.modal-body input').val(recipient)
})

// ----------------- Myslides in trang chủ------------------------

var myIndex = 0;
carousel();

function carousel() {
  var i;
  var x = document.getElementsByClassName("mySlides");
  for (i = 0; i < x.length; i++) {
    x[i].style.display = "none";  
  }
  myIndex++;
  if (myIndex > x.length) {myIndex = 1}    
  x[myIndex-1].style.display = "block";  
  setTimeout(carousel, 4000);    
}

//---------------------- banner quảng cáo trang chủ -----------------

$(document).ready(function() {
    $('#autoWidth').lightSlider({
        autoWidth:true,
        loop:true,
        onSliderLoad: function() {
            $('#autoWidth').removeClass('cS-hidden');
        } 
    });  

    $('.menu').click(function(){
					$('.navbar2_r').toggleClass('active')
				});
  });

/*-------------------------- detail product -----------------*/

