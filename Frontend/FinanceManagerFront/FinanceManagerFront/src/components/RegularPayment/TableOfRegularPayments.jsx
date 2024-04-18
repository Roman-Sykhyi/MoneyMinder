import React from "react";

const TableOfRegularPayments = (props) => {
    const getDateString = function (dateTime) {
        let date = new Date(dateTime);

        return (
            date.getDate() + "." + (date.getMonth() + 1) + "." + date.getFullYear()
        );
    };

    return (
        <div className="flex-column">
            <h2 className="text-center d-block" style={{ maxWidth: "500px" }}>
                {props.title}
            </h2>
            <div className="tableFixHead">
                <table
                    className="table  table-hover table-sm"
                    style={{ maxWidth: "500px" }}
                >
                    <thead>
                        <tr>
                            <th scope="col">#</th>
                            <th scope="col">Name</th>
                            <th scope="col">Amount</th>
                            <th scope="col">Next transaction</th>
                            <th scope="col">Period</th>
                            <th scope="col">Delete</th>
                        </tr>
                    </thead>
                    <tbody>
                        {props.payments.map((payment, index) => (
                            <tr>
                                <th scope="row" className="align-middle">
                                    {index + 1}
                                </th>
                                <td className="align-middle">{payment.name}</td>
                                <td className="align-middle">{payment.amount}</td>
                                <td className="align-middle">
                                    {getDateString(payment.start)}
                                </td>
                                <td className="align-middle">{payment.period}</td>
                                <td>
                                    <div className="d-inline-block">
                                        <button
                                            type="button"
                                            className="btn btn-danger text-left"
                                            onClick={() => props.delete(payment.id)}
                                        >
                                            X
                                        </button>
                                    </div>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    );
};

export default TableOfRegularPayments;
