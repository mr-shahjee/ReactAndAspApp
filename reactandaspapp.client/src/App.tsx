 


import React from 'react';
import CustomerList from './components/CustomerList';

function App() {
    return (
        <div style={{ maxWidth: 1000, margin: '0 auto', padding: 20 }}>
            <h1>ProductBox — Customer Management</h1>
            <CustomerList />
        </div>
    );
}

export default App;
