NomSecion = 'Index'
$(document).bind("keydown", function (e) {
    var $prev, $next, $current = $("ul li.itemselected");
	
    if (e.which === 40 && !$current.length) {
		$("ul li:first").addClass("itemselected");
    } else if (e.which === 40) {
		console.log('down');
		if(ModalAct == 1){
			ModalAct = 2;
			CaseClick = 3;
			$('#ActiveModal1').removeClass("itemselected");
			$('#ActiveModal2').removeClass("itemselected");
			$('#ActiveModal3').addClass("active");

			$('#ActiveModalInfo1').removeClass("itemselected");
			$('#ActiveModalInfo2').removeClass("itemselected");
			$('#ActiveModalInfo4').removeClass("itemselected");
			$('#ActiveModalInfo3').addClass("active");
			i = 0;
		}
    } else if (e.which === 39) {
		console.log('right');
		var valor = getCookie(NomSecion);
		CaseClick = 2;
		if(ModalAct == 1){
			$('#ActiveModal1').removeClass("itemselected");
			$('#ActiveModal2').addClass("itemselected");	

			i = i + 1
			if(i == 1){
				$('#ActiveModalInfo1').removeClass("itemselected");
				$('#ActiveModalInfo2').addClass("itemselected");
			}else if(i == 2){
				CaseClick = 4;
				$('#ActiveModalInfo2').removeClass("itemselected");
				$('#ActiveModalInfo4').addClass("itemselected");
			}else
			i = 2
			$('#ActiveModalInfo3').removeClass("active");	
		}else if(ModalAct == 0){
			$next = $current.next("li");
			if ($next.length) {
				$current.removeClass("itemselected");
				$next.addClass("itemselected");
			}
		}

    } else if (e.which === 37) {
        console.log('left');
		if(ModalAct == 1){
			CaseClick = 1;
			$('#ActiveModal1').addClass("itemselected");
			$('#ActiveModal2').removeClass("itemselected");

			//$('#ActiveModalInfo1').addClass("itemselected");
			//$('#ActiveModalInfo2').removeClass("itemselected");
			i = i - 1
			if(i == 1){
				CaseClick = 2;
				$('#ActiveModalInfo2').addClass("itemselected");
				$('#ActiveModalInfo4').removeClass("itemselected");
			}else if(i == 0){
				$('#ActiveModalInfo1').addClass("itemselected");
				$('#ActiveModalInfo2').removeClass("itemselected");
			}else
				i = 0
			$('#ActiveModalInfo3').removeClass("active");	
		}else if(ModalAct == 0){
			$prev = $current.prev("li");
			if ($prev.length) {
				$current.removeClass("itemselected");
				$prev.addClass("itemselected");
			}
		}
    } else if (e.which === 38) {
        console.log('up');
		if(ModalShow == true)
			ModalAct = 1;
		if(ModalAct == 1){
			CaseClick = 1;
			$('#ActiveModal3').removeClass("active");
			$('#ActiveModal2').removeClass("itemselected");
			$('#ActiveModal1').addClass("itemselected");
			$('#ActiveModalInfo1').addClass("itemselected");
			$('#ActiveModalInfo2').removeClass("itemselected");
			$('#ActiveModalInfo4').removeClass("itemselected");
			$('#ActiveModalInfo3').removeClass("active");	
		}
    } else if (e.which === 13) {
    	e.preventDefault();
		var seleccionado=$('li[class*="itemselected"]');
		setCookie(NomSecion, seleccionado.attr("opcion"), 7);
    	switch(seleccionado.attr("opcion")){
    		case "tv":
    			console.log("tv");
					if(ModalShow == false)
						InfoTelo();
					else{	
						if(CaseClick == 1)
							window.location.replace("eventos.html");
						else if(CaseClick == 2 )
							window.location.replace("lugares.html");
						else if(CaseClick == 4 )
							window.location.replace("instalaciones.html");
						else if(CaseClick == 3 ){
							ModalAct = 0
							ModalShow = false;
							$("#acceptModal2").modal('hide');
							$('#ActiveModalInfo1').removeClass("itemselected");
							$('#ActiveModalInfo2').removeClass("itemselected");
							$('#ActiveModalInfo4').removeClass("itemselected");
							$('#ActiveModalInfo3').removeClass("active");
						}
					}
    			break;
    		case "servicios":
    			window.location.replace("servicios.html");
    			break;
    		case "tienda":
    			console.log("tienda");
    			window.location.replace("tienda.html");
    			break;
    		case "serviciosexternos":
    			console.log("serviciosexternos");
				window.location.replace("clima.html");
    			break;
    		case "bandeja":
				window.location.replace("mensajes.html");
    			break;
			case "cuenta":
				if(ModalShow == false)
					valor()
				else{					
					if(CaseClick == 1)
						window.location.replace("lreserva.html");
					else if(CaseClick == 2 )
						window.location.replace("cuenta.html");
					else if(CaseClick == 3 ){
						ModalAct = 0
						ModalShow = false;
						$("#confirmModal").modal('hide');
						$('#ActiveModal3').removeClass("active");
						$('#ActiveModal2').removeClass("active");
						$('#ActiveModal1').removeClass("active");
					}
				}
		}	

    }
});
var CaseClick = 1;
var ModalAct = 0;
var i = 0;
var ModalShow = false;
function InfoTelo(){
	ModalAct = 1;
	$('#ActiveModalInfo1').addClass("itemselected");
	$("#acceptModal2").modal('show');
	ModalShow = true;
}
function valor(){
	ModalAct = 1;
	$('#ActiveModal1').addClass("itemselected");
	$("#confirmModal").modal('show');
	ModalShow = true;
}

$(function(){
	  var actualizarHora = function(){
	    var fecha = new Date(),
	        hora = fecha.getHours(),
	        minutos = fecha.getMinutes(),
	        segundos = fecha.getSeconds(),
	        diaSemana = fecha.getDay(),
	        dia = fecha.getDate(),
	        mes = fecha.getMonth(),
	        anio = fecha.getFullYear(),
	        ampm;
	    
	    var $pHoras = $("#horas"),
	        $pSegundos = $("#segundos"),
	        $pMinutos = $("#minutos"),
	        $pAMPM = $("#ampm"),
	        $pDiaSemana = $("#diaSemana"),
	        $pDia = $("#dia"),
	        $pMes = $("#mes"),
	        $pAnio = $("#anio");
	    var semana = ['Domingo','Lunes','Martes','Miercoles','Jueves','Viernes','Sabado'];
	    var meses = ['enero','febrero','marzo','abril','mayo','junio','julio','agosto','septiembre','octubre','noviembre','diciembre'];
	    
	    $pDiaSemana.text(semana[diaSemana]);
	    $pDia.text(dia);
	    $pMes.text(meses[mes]);
	    $pAnio.text(anio);
	    $pHoras.text(hora);
	    $pMinutos.text(minutos);
	 /*   if(hora>=12){
	      hora = hora - 12;
	      ampm = "PM";
	    }else{
	      ampm = "AM";
	    }
	    if(hora == 0){
	      hora = 12;
	    }
	    if(hora<10){$pHoras.text("0"+hora)}else{$pHoras.text(hora)};
	    if(minutos<10){$pMinutos.text("0"+minutos)}else{$pMinutos.text(minutos)};
	    if(segundos<10){$pSegundos.text("0"+segundos)}else{$pSegundos.text(segundos)};
	    $pAMPM.text(ampm);
	    */
	  };
	  
	  
	  actualizarHora();
	  var intervalo = setInterval(actualizarHora,1000);
	});

showPreloader();
$.ajax({
	url: 'https://tp-ires-api.azurewebsites.net/v1/configuracion/1',
	type: 'GET',
	async: true,
}).done(function(data){
	value = data.result;
	$("#imglogo").attr('src', value.ImgLogo);
	$('body').css('background-image', 'url("'+value.ImgFondo+'")');
	hidePreloader();
});

$.ajax({
	url: 'https://api.darksky.net/forecast/4dfacd70456b80963a3da6e0d7008666/-12.0431800,-77.0282400?lang=es&units=auto&exclude=minutely,hourly',
	type: 'GET',
	async: true,
	crossDomain: true,
	dataType: 'jsonp'
}).done(function(data){
	value = data.currently;
	$(".temp").text(Math.round(value.temperature)+" °C")
});

function showPreloader() {
	$("#loader-wrapper").removeClass("Oculto").addClass("Activo");
}

function hidePreloader() {
	$("#loader-wrapper").removeClass("Activo").addClass("Oculto");
}