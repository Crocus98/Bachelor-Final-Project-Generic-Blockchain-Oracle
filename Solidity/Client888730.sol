pragma solidity ^0.7.3;
//SPDX-License-Identifier: MIT

import "./Oracle888730.sol";

contract Client888730 {
    address public clientOwner;
    address payable public oracleAddress;
    address payable public secondAddress;
    Value public lastResponse;

    struct Value {
        string service;
        uint serviceType;
        string value;
    }

    constructor(address payable _oracleAddress, address payable _secondAddress){
        clientOwner = msg.sender;
        oracleAddress = _oracleAddress;
        secondAddress = _secondAddress;
        lastResponse = Value("NO",0,"0");
    }

    modifier onlyClientOwner() {
        require ( msg.sender == clientOwner, "You are not the owner of the client contract" );
        _;
    }

    modifier onlyOracle() {
        require ( msg.sender == oracleAddress || msg.sender == secondAddress, "You are not the oracle" );
        _;
    }

    event ResponseEvent(
        string _service,
        uint _type,
        string _value
    ); 

    function GetResponse(string calldata _service, uint  _serviceType, string calldata _value) external payable onlyOracle{
        lastResponse = Value(_service, _serviceType, _value);
        emit ResponseEvent(_service, _serviceType, _value);
    }

    function SendSubscribeRequest(string memory _subscribeService, uint _subscribeServiceType) public onlyClientOwner{
        Oracle888730 oracle = Oracle888730(oracleAddress);
        oracle.GetSubscribeRequest(_subscribeService, _subscribeServiceType);
    }

    function SendRequest(string memory _requestService, uint _requestServiceType) public onlyClientOwner{
        Oracle888730 oracle = Oracle888730(oracleAddress);
        oracle.GetRequest(_requestService, _requestServiceType);
    }

}