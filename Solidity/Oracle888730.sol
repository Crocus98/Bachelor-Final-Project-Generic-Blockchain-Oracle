pragma solidity ^0.7.3;
//SPDX-License-Identifier: MIT

//pragma experimental ABIEncoderV2;


import "./Client888730.sol";


//Matteo Savino
contract Oracle888730 {
    address payable public oracleOwner;
    string public OracleName = "GenericOracle";

    constructor() {
        oracleOwner = msg.sender;
    }

    event GenericPaymentEvent(
        address _sender,
        uint _value
    );

    event ServicePaymentEvent(
        address _sender,
        uint _value,
        string _serviceType
    );

    event RequestEvent(
         address _sender,
         uint _requestType
    );

    event SubscribeEvent(
        address _sender,
        uint _requestType
    );

    modifier onlyOracleOwner() {
        require ( msg.sender == oracleOwner, "You are not the owner of the oracle contract" );
        _;
    }

    //receive function che viene chiamata solo se si inviano ether senza alcun dato
    receive() external payable {
        //I pagamenti ricevuti saranno tenuti come donazione
        oracleOwner.transfer(msg.value);
        emit GenericPaymentEvent(msg.sender, msg.value);
        //Queste f hanno limite di 2300 gas limit non si può ne trasferire ne scrivere su chain
        //oracleOwner.transfer(msg.value);
    }

    //Fallback viene chiamata solo se non esiste nessun'altra funzione payable (ultima spiaggia)
    fallback() external payable {
        //I pagamenti ricevuti saranno tenuti come donazione
        oracleOwner.transfer(msg.value);
        emit GenericPaymentEvent(msg.sender, msg.value);
        //Queste f hanno limite di 2300 gas limit non si può ne trasferire ne scrivere su chain
    }

    function PayService(string calldata _service) external payable {
        //Attenzione se utilizzo .call invece di .transfer per evitare falle di sicurezza
        //https://ethereum.stackexchange.com/questions/68861/why-cant-we-use-transfer-to-get-a-fallback-from-a-contract
        emit ServicePaymentEvent (msg.sender, msg.value, _service);
        oracleOwner.transfer(msg.value);
    }

    //Push based inbound oracle request
    function GetSubscribeRequest(uint _requestType) external payable {
        emit SubscribeEvent(msg.sender, _requestType);
    }

    //Pull based inbound oracle request
    function GetRequest(uint _requestType) external payable {
        emit RequestEvent(msg.sender, _requestType);
    }
    
    //Response
    function SendResponse(address _clientAddress, string memory _value, uint _requestType) public onlyOracleOwner {
        //_value potrebbe essere un JSON
        Client888730 client = Client888730(address(_clientAddress));
        client.GetResponse(_value, _requestType);
    }

    //Public: functions are part of the contract interface and can be either called internally or via messages. For public state variables, an automatic getter function (see below) is generated.
    //Internal: functions and state variables can only be accessed internally (i.e. from within the current contract or contracts deriving from it), without using this
    //Private: Private functions and state variables are only visible for the contract they are defined in and not in derived contracts.
    //External: they can be called from other contracts and via transactions. An external function f cannot be called internally (i.e. f() does not work, but this.f() works).
}