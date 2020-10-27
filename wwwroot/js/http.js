const processResponse = resp => {

    return new Promise((resolve, reject) => {

        if (resp.status == 400)
            resp.text().then(err => {
                if(String(err).includes('"status":'))
                    reject(JSON.parse(err));
                else
                    reject(err);
            } );


        if (resp.status == 200)
            resp.json().then(data => resolve(data))

    })

}

const http = url => {

    let base = (url, properties) => {
        return new Promise((resolve, reject) => {

            fetch(`${url}`, properties)
                .then(processResponse)
                .catch(error => reject(error))
                .then(response => resolve(response));

        })
    };

    let getParameters = function(data) {
        let queryString = '';

        if (data) {
            let parameters = '';

            if (typeof data == 'function')
                parameters = data();
            else
                parameters = data;

            for (const prop in parameters) {
                queryString += `&${prop}=${parameters[prop]}`
            }

        }

        return queryString.length > 0 ? queryString.replace('&', '?') : queryString;

    };


    const _url = `${path}${url}`;

    return {

        asGet: (data = null) => {

            let params = getParameters(data);

            return base(`${_url}${params}`, { method: 'GET' });

        },
        asPost: data => {
            return new Promise((resolve, reject) => {
                fetch(_url, {
                        method: 'POST',
                        body: data ? JSON.stringify(data) : null,
                        headers: {
                            "Content-Type": "application/json;charset=UTF-8"
                        }
                    })
                    .then(processResponse)
                    .catch(error => reject(error))
                    .then(response => resolve(response));
            })
        },
        asDelete: (data = null) => {
            let params = getParameters(data);

            return base(`${_url}${params}`, { method: 'DELETE' });

        },
        asFile: (file = null) => {
            return new Promise(resolve => {
                let formData = new FormData();
                formData.append('file', file);
                fetch(_url, {
                        method: 'POST',
                        body: formData
                    })
                    .then(response => response.json())
                    .then(response => resolve(response))
                    .catch(error => console.error('Error:', error));
            })
        },
    }
}

export default http;