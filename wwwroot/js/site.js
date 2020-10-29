import http from './http.js';
import { selector } from './manager.js';

const generateUid = length =>
{
    let result = '';
    let alpha = 'ABCDEGFHIJKLMNOPQRSTUVWXZY';
    let characters = alpha + alpha.toLocaleLowerCase() + '0123456789';
    let MyLength = characters.length;
    for (let index = 0; index < length; index++) {
        result += characters.charAt(Math.floor(Math.random() * MyLength));
        
    }
    return result;
}

const getMatch = () => selector('matchId');


const userId = generateUid(15);
const match = getMatch();

if(match){
    setInterval(() =>
    {        
        http(`admin/match/${match.value}/viewer/${userId}`).asGet().then(total =>{
            selector('viendo').innerHTML = total + ' viendo'
        });
    },20000)
}