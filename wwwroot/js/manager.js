import http from "./http.js";

const selector = target => document.querySelector(`#${target}`);

const viewMatchNow = id => `<a style="margin-top: 10px;" href="/matches/stream/${id}" class="btn btn-outline-secondary btn-block">Ver ahora</a>`;

const viewMatchesNext = id => `<div class="container">
<h1 id="headline">Faltan:</h1>
<div id="countdown">
  <ul>
    <li><span id="days"></span>dias</li>
    <li><span id="hours"></span>horas</li>
    <li><span id="minutes"></span>minutos</li>
    <li><span id="seconds"></span>segundos</li>
  </ul>
</div>
</div>`;

let setCountDown = (date,id) => {
    const second = 1000,
        minute = second * 60,
        hour = minute * 60,
        day = hour * 24;

    let birthday = date,
        countDown = new Date(birthday).getTime(),
        x = setInterval(function () {

            let now = new Date().getTime(),
                distance = countDown - now;

            document.getElementById("days").innerText = Math.floor(distance / (day)),
                document.getElementById("hours").innerText = String(Math.floor((distance % (day)) / (hour))).padStart(2, "0"),
                document.getElementById("minutes").innerText = String(Math.floor((distance % (hour)) / (minute))).padStart(2, "0"),
                document.getElementById("seconds").innerText = Math.floor((distance % (minute)) / second);

            //do something later when date is reached
            if (distance < 0) {
               
                clearInterval(x);

                window.location = path + 'matches/stream/'+id
            }
            //seconds
        }, 0)
}

const isToday = date => {
    let now = new Date();

}
const tableNext = match =>{
    return `<table style="border: solid 1px #efefef;">
    <tr>
        <td width="150px" style="text-align: center;">${match.teamHome}</td>
        <td width="50px" style="text-align: center;">VS</td>
        <td width="150px" style="text-align: center;">${match.teamAway}</td>
    </tr>
    <tr>
        <td width="150px" style="text-align: center;"><img width="100px" src="/images/${match.teamHomeImageName}" alt=""></td>
        <td width="50px" style="text-align: center;">${moment(match.eventDate).calendar()}</td>
        <td width="150px" style="text-align: center;"><img width="100px" src="/images/${match.teamAwayImageName}" alt=""></td>
    </tr>
    <tr style="border-top: solid 1px #efefef;">
        <td colspan="3" style="text-align: center;"><a href="/matches/stream/${match.id}" class="btn btn-outline-secondary btn-block">Ver</a></td>
    </tr>
</table>`;
}

const nextMatches = (id=0) => {

    http('live/coming/' + id).asGet().then(data => {
        if(data){        
            let div = selector('matchesNext');
            let html = '';
            data.forEach(match =>{
                html +=  tableNext(match);
            });
            
            if(html.length > 0)
                div.innerHTML = html;
        }
    });

}

export { selector, viewMatchNow, viewMatchesNext, setCountDown, tableNext, nextMatches }