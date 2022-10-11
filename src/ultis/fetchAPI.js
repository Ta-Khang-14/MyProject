export default async function fetchData(api, method = "GET", jwt = "", body) {
    try {
        let response = await fetch(api, {
            method,
            headers: {
                "Content-Type": "application/json",
                Authorization: `Bearer ${jwt}`,
                "X-Custom-Header": "header value",
                "Access-Control-Allow-Origin": "*",
            },
            body: JSON.stringify(body),
        });
        let data = await response.json();

        return data;
    } catch (error) {
        console.log("Có lỗi: ", error);
        return { statusCode: 1 };
    }
}
