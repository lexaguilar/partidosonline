const selector = target => document.querySelector(`#${target}`);

const viewMatchNow = id => `<a style="margin-top: 10px;" href="/home/live/${id}" class="btn btn-outline-secondary btn-block">Ver ahora</a>`;

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
<div class="message">
  <div id="content">
    <span class="emoji"></span>
    <span class="emoji"></span>
    <span class="emoji"></span>
  </div>
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
                document.getElementById("hours").innerText = Math.floor((distance % (day)) / (hour)),
                document.getElementById("minutes").innerText = Math.floor((distance % (hour)) / (minute)),
                document.getElementById("seconds").innerText = Math.floor((distance % (minute)) / second);

            //do something later when date is reached
            if (distance < 0) {
                let headline = document.getElementById("headline"),
                    countdown = document.getElementById("countdown"),
                    content = document.getElementById("content");

                // headline.innerText = "It's my birthday!";
                // countdown.style.display = "none";
                // content.style.display = "block";

                clearInterval(x);

                window.location = path + 'home/live/'+id
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
        <td colspan="3" style="text-align: center;"><a href="/home/live/${match.id}" class="btn btn-outline-secondary btn-block">Ver</a></td>
    </tr>
</table>`;
}

export { selector, viewMatchNow, viewMatchesNext, setCountDown, tableNext }