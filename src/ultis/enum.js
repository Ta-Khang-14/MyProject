const EnumMisa = {
    Gender: {
        Male: 0,
        Female: 1,
        Other: 2,
    },
    FormMode: {
        New: 0,
        Edit: 1,
        Watch: 2,
    },
    KeyCode: {
        Enter: 13,
        ESC: 27,
        UpArrow: 38,
        DownArrow: 40,
    },
    PopUp: {
        Warning: 1,
        Question: 2,
        Action: {
            Delete: 1,
            New: 2,
            Update: 3,
        },
    },
    Validate: {
        Required: 1,
        Email: 2,
        Phone: 3,
        StringUTF8: 4,
        Number: 5,
    },
};

export default EnumMisa;
